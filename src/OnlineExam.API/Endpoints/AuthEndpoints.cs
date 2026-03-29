using Carter;
using MediatR;
using OnlineExam.Application.Auth.Commands;

namespace OnlineExam.API.Endpoints;

public class AuthEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/auth").WithTags("Auth");

        group.MapPost("/register", async (RegisterCommand command, ISender sender) =>
        {
            var result = await sender.Send(command);
            return Results.Created($"/api/auth/{result.UserId}", result);
        });

        group.MapPost("/login", async (LoginCommand command, ISender sender) =>
        {
            try
            {
                var result = await sender.Send(command);
                return Results.Ok(result);
            }
            catch (UnauthorizedAccessException)
            {
                return Results.Unauthorized();
            }
        });
    }
}
