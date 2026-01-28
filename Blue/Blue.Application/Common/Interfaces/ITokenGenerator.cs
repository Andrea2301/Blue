using Blue.Domain.Users;

namespace Blue.Application.Common.Interfaces;

public interface ITokenGenerator
{
    string GenerateAccessToken(User user);
    string GenerateRefreshToken();
}