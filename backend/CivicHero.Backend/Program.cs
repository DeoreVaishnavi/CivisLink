using CivicHero.Backend.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

// ------------------------------------------------------------
// Services
// ------------------------------------------------------------

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerDocumentation();

builder.Services.AddInfrastructure(builder.Configuration);

// ------------------------------------------------------------
// Build Application
// ------------------------------------------------------------

var app = builder.Build();

// ------------------------------------------------------------
// Middleware Pipeline
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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();