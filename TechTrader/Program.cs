using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Json;
using TechTrader.Endpoints;
using TechTrader.Interfaces;
using TechTrader.Services;
using TechTrader.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Allow health checks
builder.Services.AddHealthChecks();

// Allows passing datetimes without time zone data 
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// Allows API endpoints to access the database through Entity Framework Core
var connectionString = builder.Configuration["TechTraderDbConnectionString"];
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
        policy.WithOrigins("http://localhost:3000", "https://techtraderclient-production.up.railway.app")
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

// Use CORS
app.UseCors();

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

// Map endpoints
app.MapCategoryEndpoints();
app.MapConditionEndpoints();
app.MapListingEndpoints();
app.MapMessageEndpoints();
app.MapPaymentTypeEndpoints();
app.MapSavedListingEndpoints();
app.MapUserEndpoints();

app.Run();