using MediatR;
using OnlineExam.Application.Common;
using OnlineExam.Domain.Entities;
using OnlineExam.Domain.Enums;
using OnlineExam.Domain.Interfaces;

namespace OnlineExam.Application.Auth.Commands;

public class RegisterCommandHandler(
    IRepository<User> userRepo,
    ITokenService tokenService
) : IRequestHandler<RegisterCommand, AuthResult>
{
    public async Task<AuthResult> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var existing = await userRepo.FirstOrDefaultAsync(u => u.Email == request.Email);
        if (existing is not null)
            throw new InvalidOperationException("Email already registered");

        var user = new User
        {
            Username = request.Username,
            Email = request.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
            FirstName = request.FirstName,
            LastName = request.LastName,
            Role = UserRole.Student,
            IsActive = true
        };

        var saved = await userRepo.AddAsync(user);
        var token = tokenService.GenerateToken(saved);

        return new AuthResult(saved.Id, saved.Username, saved.Email, saved.Role.ToString(), token);
    }
}

public class LoginCommandHandler(
    IRepository<User> userRepo,
    ITokenService tokenService
) : IRequestHandler<LoginCommand, AuthResult>
{
    public async Task<AuthResult> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepo.FirstOrDefaultAsync(u => u.Email == request.Email);
        if (user is null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            throw new UnauthorizedAccessException("Invalid email or password");

        if (!user.IsActive)
            throw new UnauthorizedAccessException("Account is deactivated");

        var token = tokenService.GenerateToken(user);

        return new AuthResult(user.Id, user.Username, user.Email, user.Role.ToString(), token);
    }
}
