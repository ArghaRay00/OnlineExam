using Carter;
using Microsoft.EntityFrameworkCore;
using OnlineExam.Infrastructure.Data;
using Serilog;
using Scalar.AspNetCore;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);
    builder.Host.UseSerilog();

    // Database
    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

    // OpenAPI + Scalar
    builder.Services.AddOpenApi();

    // Carter (endpoint modules)
    builder.Services.AddCarter();

    // CORS
    builder.Services.AddCors(options =>
        options.AddDefaultPolicy(policy =>
            policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

    var app = builder.Build();

    app.UseSerilogRequestLogging();
    app.UseCors();

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
