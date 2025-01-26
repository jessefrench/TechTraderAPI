using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Json;
using TechTrader.Endpoints;
using TechTrader.Interfaces;
using TechTrader.Services;
using TechTrader.Repositories;
using TechTrader.Utility;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Allow health checks
builder.Services.AddHealthChecks();

// Allows passing datetimes without time zone data 
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// Retrieve the connection string
var connectionString = ConnectionHelper.GetConnectionString(builder.Configuration);
builder.Services.AddDbContext<TechTraderDbContext>(options => options.UseNpgsql(connectionString));

// Allows API endpoints to access the database through Entity Framework Core
builder.Services.AddNpgsql<TechTraderDbContext>(connectionString);

// Set the JSON serializer options
builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:3000")
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials();
    });
});

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

var app = builder.Build();

// Endpoints
app.MapCategoryEndpoints();
app.MapConditionEndpoints();
app.MapListingEndpoints();
app.MapMessageEndpoints();
app.MapPaymentTypeEndpoints();
app.MapSavedListingEndpoints();
app.MapUserEndpoints();

// Use health checks
app.UseHealthChecks("/health");

// Use CORS
app.UseCors();

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

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<TechTraderDbContext>();

    // Apply any pending migrations to the database
    await context.Database.MigrateAsync();

    // Run additional data management tasks
    await DataHelper.ManageDataAsync(scope.ServiceProvider);
}

app.Run();