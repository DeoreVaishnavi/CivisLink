using CivicHero.Backend.Infrastructure.Extensions;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(new ConfigurationBuilder()
        .AddJsonFile("appsettings.json")
        .AddJsonFile("appsettings.Development.json", optional: true)
        .AddEnvironmentVariables()
        .Build())
    .CreateLogger();

try
{
    Log.Information("Starting CivicHero API...");

    var builder = WebApplication.CreateBuilder(args);

    // ------------------------------------------------------------
    // Logging
    // ------------------------------------------------------------

    builder.AddApplicationLogging();

    // ------------------------------------------------------------
    // Services
    // ------------------------------------------------------------

    builder.Services.AddControllers();

    builder.Services.AddEndpointsApiExplorer();

    builder.Services.AddSwaggerDocumentation();

    builder.Services.AddInfrastructure(builder.Configuration);

    // ------------------------------------------------------------
    // Build
    // ------------------------------------------------------------

    var app = builder.Build();

    // ------------------------------------------------------------
    // Swagger
    // ------------------------------------------------------------

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();

        app.UseSwaggerUI(options =>
        {
            options.DocumentTitle = "CivicHero API";

            options.SwaggerEndpoint(
                "/swagger/v1/swagger.json",
                "CivicHero API v1");

            options.RoutePrefix = "swagger";
        });
    }

    // ------------------------------------------------------------
    // Middleware
    // ------------------------------------------------------------

    app.UseHttpsRedirection();

    app.UseAuthorization();

    // ------------------------------------------------------------
    // Endpoints
    // ------------------------------------------------------------

    app.MapControllers();

    app.MapHealthChecks("/health", new HealthCheckOptions
    {
        AllowCachingResponses = false
    });

    Log.Information("CivicHero API started successfully.");

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly.");
}
finally
{
    Log.CloseAndFlush();
}