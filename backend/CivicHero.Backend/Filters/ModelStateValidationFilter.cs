using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CivicHero.Backend.Filters;

/// <summary>
/// Validates ModelState before an action executes.
/// If validation fails, a standardized 400 Bad Request response is returned.
/// </summary>
public sealed class ModelStateValidationFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.ModelState.IsValid)
        {
            return;
        }

        var errors = context.ModelState
            .Where(entry => entry.Value?.Errors.Count > 0)
            .ToDictionary(
                entry => entry.Key,
                entry => entry.Value!.Errors
                    .Select(error => string.IsNullOrWhiteSpace(error.ErrorMessage)
                        ? "Invalid value."
                        : error.ErrorMessage)
                    .ToArray());

        context.Result = new BadRequestObjectResult(new
        {
            Message = "Validation failed.",
            Errors = errors
        });
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        // Nothing to do after the action executes.
    }
}