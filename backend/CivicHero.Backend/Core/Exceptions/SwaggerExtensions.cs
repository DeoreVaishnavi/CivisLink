using System.Reflection;
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
    /// <param name="services">The service collection.</param>
    /// <returns>The updated service collection.</returns>
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
                    Description = "REST API for the CivicHero Smart Civic Complaint Management Platform.",
                    Contact = new OpenApiContact
                    {
                        Name = "CivicHero Development Team"
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Internal Use"
                    }
                });

            // Enable XML documentation if available.
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

            if (File.Exists(xmlPath))
            {
                options.IncludeXmlComments(xmlPath);
            }

            // Sort endpoints alphabetically.
            options.OrderActionsBy(api => api.RelativePath);
        });

        return services;
    }
}