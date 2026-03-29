using OnlineExam.Domain.Entities;

namespace OnlineExam.Application.Common;

public interface ITokenService
{
    string GenerateToken(User user);
}
