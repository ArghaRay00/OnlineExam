using Carter;
using FluentValidation;
using OnlineExam.Infrastructure;
using Serilog;
using Scalar.AspNetCore;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);
    builder.Host.UseSerilog();

    // Infrastructure (DB, repos, JWT, auth)
    builder.Services.AddInfrastructure(builder.Configuration);

    // MediatR + FluentValidation
    builder.Services.AddMediatR(cfg =>
        cfg.RegisterServicesFromAssembly(typeof(OnlineExam.Application.Auth.Commands.RegisterCommand).Assembly));
    builder.Services.AddValidatorsFromAssembly(typeof(OnlineExam.Application.Auth.Commands.RegisterCommandValidator).Assembly);

    // OpenAPI + Scalar
    builder.Services.AddOpenApi();

    // Carter
    builder.Services.AddCarter();

    // CORS
    builder.Services.AddCors(options =>
        options.AddDefaultPolicy(policy =>
            policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

    var app = builder.Build();

    app.UseSerilogRequestLogging();
    app.UseCors();
    app.UseAuthentication();
    app.UseAuthorization();

    // Scalar API docs at /scalar
    app.MapOpenApi();
    app.MapScalarApiReference();

    // Carter endpoints
    app.MapCarter();

    // Health check
    app.MapGet("/", () => new { status = "Online Exam API is running", version = "2.0.0" });

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}
