using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using OnlineExam.Application.Common;
using OnlineExam.Domain.Interfaces;
using OnlineExam.Infrastructure.Auth;
using OnlineExam.Infrastructure.Data;

namespace OnlineExam.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        // Database
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(config.GetConnectionString("DefaultConnection")));

        // Generic repository
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        // JWT
        services.AddScoped<ITokenService, JwtTokenService>();

        var jwtSecret = config["Jwt:Secret"] ?? "default-dev-secret-change-in-production-32chars!";
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = config["Jwt:Issuer"] ?? "OnlineExam",
                    ValidAudience = config["Jwt:Audience"] ?? "OnlineExam",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret))
                };
            });

        services.AddAuthorization();

        return services;
    }
}
