using System.Net;
using System.Text.Json;
using CivicHero.Backend.Core.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace CivicHero.Backend.Middleware;

/// <summary>
/// Handles all unhandled exceptions and converts them into
/// standardized JSON responses.
/// </summary>
public sealed class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionMiddleware> _logger;

    public GlobalExceptionMiddleware(
        RequestDelegate next,
        ILogger<GlobalExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (BusinessRuleViolationException ex)
        {
            _logger.LogWarning(ex, "Business rule violation.");

            await WriteErrorResponseAsync(
                context,
                HttpStatusCode.BadRequest,
                ex.Message);
        }
        catch (DomainException ex)
        {
            _logger.LogWarning(ex, "Domain exception.");

            await WriteErrorResponseAsync(
                context,
                HttpStatusCode.BadRequest,
                ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception.");

            await WriteErrorResponseAsync(
                context,
                HttpStatusCode.InternalServerError,
                "An unexpected error occurred.");
        }
    }

    private static async Task WriteErrorResponseAsync(
        HttpContext context,
        HttpStatusCode statusCode,
        string message)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        var response = new ErrorResponse
        {
            StatusCode = (int)statusCode,
            Message = message
        };

        await context.Response.WriteAsync(
            JsonSerializer.Serialize(response));
    }

    private sealed class ErrorResponse
    {
        public int StatusCode { get; init; }

        public string Message { get; init; } = string.Empty;
    }
}