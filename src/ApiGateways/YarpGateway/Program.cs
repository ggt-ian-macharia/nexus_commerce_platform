using System.Threading.RateLimiting;
using Polly;

var builder = WebApplication.CreateBuilder(args);

// Add Rate Limiting
builder.Services.AddRateLimiter(options =>
{
    // Fixed window limiter for Orders - 100 requests per 10 seconds
    options.AddPolicy("order-rate-limit", context =>
        RateLimitPartition.GetFixedWindowLimiter(
            partitionKey: context.Connection.RemoteIpAddress?.ToString() ?? "anonymous",
            factory: _ => new FixedWindowRateLimiterOptions
            {
                Window = TimeSpan.FromSeconds(10),
                PermitLimit = 100,
                QueueLimit = 20
            }));
    
    // Sliding window for Catalog - 200 requests per 10 seconds
    options.AddPolicy("catalog-rate-limit", context =>
        RateLimitPartition.GetSlidingWindowLimiter(
            partitionKey: context.Connection.RemoteIpAddress?.ToString() ?? "anonymous",
            factory: _ => new SlidingWindowRateLimiterOptions
            {
                Window = TimeSpan.FromSeconds(10),
                PermitLimit = 200,
                SegmentsPerWindow = 4,
                QueueLimit = 50
            }));
    
    // Global rate limiter - 500 requests per second
    options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(context =>
    {
        return RateLimitPartition.GetFixedWindowLimiter("global", _ => new FixedWindowRateLimiterOptions
        {
            Window = TimeSpan.FromSeconds(1),
            PermitLimit = 500,
            QueueLimit = 100
        });
    });
    
    // Rejection response
    options.OnRejected = async (context, token) =>
    {
        context.HttpContext.Response.StatusCode = 429;
        await context.HttpContext.Response.WriteAsync("Too many requests. Please try again later.", token);
    };
});

// Add HTTP client with resilience (Circuit Breaker + Retry + Timeout)
builder.Services.AddHttpClient("resilient-client")
    .AddStandardResilienceHandler(options =>
    {
        // Circuit Breaker Configuration
        options.CircuitBreaker.SamplingDuration = TimeSpan.FromSeconds(30);
        options.CircuitBreaker.FailureRatio = 0.5; // Open circuit if 50% of requests fail
        options.CircuitBreaker.MinimumThroughput = 10; // Minimum 10 requests before evaluating
        options.CircuitBreaker.BreakDuration = TimeSpan.FromSeconds(30); // Stay open for 30s
        
        // Retry with Exponential Backoff
        options.Retry.MaxRetryAttempts = 3;
        options.Retry.BackoffType = DelayBackoffType.Exponential; // 2^n seconds
        options.Retry.UseJitter = true; // Add randomness to prevent thundering herd
        options.Retry.Delay = TimeSpan.FromSeconds(1); // Base delay
        
        // Timeout Configuration
        options.TotalRequestTimeout.Timeout = TimeSpan.FromSeconds(30); // Total request timeout
        options.AttemptTimeout.Timeout = TimeSpan.FromSeconds(10); // Per-attempt timeout
    });

builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

var app = builder.Build();

// Use rate limiter middleware
app.UseRateLimiter();

app.MapReverseProxy();

app.Run();
