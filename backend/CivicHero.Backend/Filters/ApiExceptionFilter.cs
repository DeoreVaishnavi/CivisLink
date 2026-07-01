using CivicHero.Backend.Core.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace CivicHero.Backend.Filters;

/// <summary>
/// Handles exceptions thrown within the ASP.NET Core MVC pipeline.
/// </summary>
public sealed class ApiExceptionFilter : IExceptionFilter
{
    private readonly ILogger<ApiExceptionFilter> _logger;

    public ApiExceptionFilter(
        ILogger<ApiExceptionFilter> logger)
    {
        _logger = logger;
    }

    public void OnException(ExceptionContext context)
    {
        switch (context.Exception)
        {
            case BusinessRuleViolationException businessException:
                _logger.LogWarning(
                    businessException,
                    "Business rule violation.");

                context.Result = new BadRequestObjectResult(new
                {
                    Message = businessException.Message
                });

                break;

            case DomainException domainException:
                _logger.LogWarning(
                    domainException,
                    "Domain exception.");

                context.Result = new BadRequestObjectResult(new
                {
                    Message = domainException.Message
                });

                break;

            default:
                _logger.LogError(
                    context.Exception,
                    "Unhandled MVC exception.");

                context.Result = new ObjectResult(new
                {
                    Message = "An unexpected error occurred."
                })
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };

                break;
        }

        context.ExceptionHandled = true;
    }
}