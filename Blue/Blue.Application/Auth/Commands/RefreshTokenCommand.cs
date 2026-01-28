using Blue.Application.Common.Interfaces;
using Blue.Application.Auth.DTOs;
using Blue.Application.Common.DTOs;
using Blue.Domain.Users;

namespace Blue.Application.Auth.Commands;

public class RefreshTokenCommand
{
    private readonly IUserRepository _users;
    private readonly ITokenGenerator _tokenGenerator;

    public RefreshTokenCommand(
        IUserRepository users,
        ITokenGenerator tokenGenerator)
    {
        _users = users;
        _tokenGenerator = tokenGenerator;
    }

    public async Task<RefreshTokenResponse> ExecuteAsync(RefreshTokenRequest request)
    {
        var user = await _users.GetByRefreshTokenAsync(request.RefreshToken);

        if (user == null || user.RefreshTokenExpiry < DateTime.UtcNow)
            throw new InvalidOperationException("Ivalid Token");

        var newAccessToken = _tokenGenerator.GenerateAccessToken(user);
        var newRefreshToken = _tokenGenerator.GenerateRefreshToken();

        user.SetRefreshToken(newRefreshToken, DateTime.UtcNow.AddDays(7));
        await _users.UpdateAsync(user);

        return new RefreshTokenResponse
        {
            AccessToken = newAccessToken,
            RefreshToken = newRefreshToken
        };
    }
}