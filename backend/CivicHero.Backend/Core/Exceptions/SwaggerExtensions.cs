using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace CivicHero.Backend.Infrastructure.Extensions;

/// <summary>
/// Extension methods for configuring Swagger/OpenAPI.
/// </summary>
public static class SwaggerExtensions
{
    /// <summary>
    /// Registers Swagger/OpenAPI services.
    /// </summary>
    public static IServiceCollection AddSwaggerDocumentation(
        this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc(
                "v1",
                new OpenApiInfo
                {
                    Title = "CivicHero API",
                    Version = "v1",
                    Description = "Backend API for the CivicHero complaint management platform.",
                    Contact = new OpenApiContact
                    {
                        Name = "CivicHero Development Team"
                    }
                });
        });

        return services;
    }
}