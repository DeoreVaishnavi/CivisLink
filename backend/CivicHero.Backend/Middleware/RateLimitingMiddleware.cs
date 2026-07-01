using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace CivicHero.Backend.Middleware;

/// <summary>
/// Middleware executed after ASP.NET Core rate limiting.
/// Responsible for adding project-specific behavior such as
/// response headers and logging.
/// </summary>
public sealed class RateLimitingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RateLimitingMiddleware> _logger;

    public RateLimitingMiddleware(
        RequestDelegate next,
        ILogger<RateLimitingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        context.Response.Headers["X-RateLimit-Policy"] = "Default";

        await _next(context);

        _logger.LogDebug(
            "Rate limiting middleware executed for {Method} {Path}",
            context.Request.Method,
            context.Request.Path);
    }
}