using Cart.Extensions;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Redis Configuration
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetValue<string>("ConnectionStrings:Redis");
});

// Add repositories, services, validators, and mappings
builder.Services.AddRepositories();
builder.Services.AddApplicationServices();
builder.Services.AddValidation();
builder.Services.AddMappings();

// Add Health Checks
builder.Services.AddHealthChecks()
    .AddRedis(
        builder.Configuration.GetValue<string>("ConnectionStrings:Redis")!,
        name: "redis",
        tags: new[] { "ready", "cache" });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

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
