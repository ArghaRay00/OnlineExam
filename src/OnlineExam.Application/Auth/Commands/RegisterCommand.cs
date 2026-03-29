using MediatR;

namespace OnlineExam.Application.Auth.Commands;

public record RegisterCommand(
    string Username,
    string Email,
    string Password,
    string FirstName,
    string LastName
) : IRequest<AuthResult>;

public record LoginCommand(
    string Email,
    string Password
) : IRequest<AuthResult>;

public record AuthResult(
    int UserId,
    string Username,
    string Email,
    string Role,
    string Token
);
