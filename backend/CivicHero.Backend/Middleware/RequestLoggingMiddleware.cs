using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace CivicHero.Backend.Middleware;

/// <summary>
/// Logs every incoming HTTP request and outgoing HTTP response.
/// </summary>
public sealed class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestLoggingMiddleware> _logger;

    public RequestLoggingMiddleware(
        RequestDelegate next,
        ILogger<RequestLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var stopwatch = Stopwatch.StartNew();

        _logger.LogInformation(
            "HTTP Request Started | {Method} {Path} | CorrelationId: {CorrelationId}",
            context.Request.Method,
            context.Request.Path,
            context.TraceIdentifier);

        await _next(context);

        stopwatch.Stop();

        _logger.LogInformation(
            "HTTP Request Finished | {Method} {Path} | StatusCode: {StatusCode} | Elapsed: {Elapsed} ms | CorrelationId: {CorrelationId}",
            context.Request.Method,
            context.Request.Path,
            context.Response.StatusCode,
            stopwatch.ElapsedMilliseconds,
            context.TraceIdentifier);
    }
}