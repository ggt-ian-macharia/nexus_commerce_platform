using EventBus;
using Order.Data;
using Order.Extensions;
using Order.Services;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });
builder.Services.AddOpenApi();

// Add Database
builder.Services.AddDatabase(builder.Configuration);

// Add repositories, services, validators, and mappings
builder.Services.AddRepositories();
builder.Services.AddApplicationServices();
builder.Services.AddValidation();
builder.Services.AddMappings();

// Add Event Bus
builder.Services.AddEventBus(builder.Configuration);

// Add Health Checks  
builder.Services.AddHealthChecks()
    .AddNpgSql(
        builder.Configuration.GetConnectionString("DefaultConnection")!,
        name: "postgres",
        tags: new[] { "ready", "db" });

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// Apply database migrations
app.ApplyMigration();

app.UseHttpsRedirection();

// Health Check Endpoints
app.MapHealthChecks("/health/live", new HealthCheckOptions
{
    Predicate = _ => false
});

app.MapHealthChecks("/health/ready", new HealthCheckOptions
{
    Predicate = check => check.Tags.Contains("ready")
});

app.MapControllers();

app.Run();
