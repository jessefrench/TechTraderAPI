using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Json;
using TechTrader.Endpoints;
using TechTrader.Interfaces;
using TechTrader.Services;
using TechTrader.Repositories;
using Microsoft.EntityFrameworkCore;
using TechTrader.Utility;

// Initialize web application builder
var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Allow health checks
builder.Services.AddHealthChecks();

// Determine environment specific connection string
string connectionString;
if (builder.Environment.IsDevelopment())
{
    // Use local database in development
    connectionString = builder.Configuration["TechTraderDbConnectionString"];
}
else
{
    // Fetch from Railway environment variable
    connectionString = Environment.GetEnvironmentVariable("DATABASE_URL");
}

// Set the database context
builder.Services.AddDbContext<TechTraderDbContext>(options =>
    options.UseNpgsql(connectionString));

// Set the JSON serializer options
builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
    options.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    options.SerializerOptions.WriteIndented = true;
    options.SerializerOptions.Converters.Add(new DateTimeHelper());
});

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:3000", "https://localhost:3000", "https://techtrader.up.railway.app")
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials();
    });
});

// Add SignalR for real time messages
builder.Services.AddSignalR();

// Add scoped services
builder.Services.AddScoped<IListingService, ListingService>();
builder.Services.AddScoped<IListingRepository, ListingRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IConditionService, ConditionService>();
builder.Services.AddScoped<IConditionRepository, ConditionRepository>();
builder.Services.AddScoped<IPaymentTypeService, PaymentTypeService>();
builder.Services.AddScoped<IPaymentTypeRepository, PaymentTypeRepository>();
builder.Services.AddScoped<IMessageService, MessageService>();
builder.Services.AddScoped<IMessageRepository, MessageRepository>();
builder.Services.AddScoped<ISavedListingService, SavedListingService>();
builder.Services.AddScoped<ISavedListingRepository, SavedListingRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

// Create web application from builder services
var app = builder.Build();

// Use CORS
app.UseCors();

// Use SignalR
app.MapHub<MessageHub>("/messageHub");

// Use health checks
app.UseHealthChecks("/health");

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Only use HTTPS redirection in development
if (app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

// Create new scope for resolving scoped services
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<TechTraderDbContext>();

    // Apply any pending migrations to the database
    await context.Database.MigrateAsync();

    // Run additional data management tasks
    await DataHelper.ManageDataAsync(scope.ServiceProvider);
}

// Map endpoints
app.MapCategoryEndpoints();
app.MapConditionEndpoints();
app.MapListingEndpoints();
app.MapMessageEndpoints();
app.MapPaymentTypeEndpoints();
app.MapSavedListingEndpoints();
app.MapUserEndpoints();

// Run application
app.Run();