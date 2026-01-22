namespace Blue.Application.Common.DTOs;

public class RefreshTokenResponse
{
    public string AccessToken { get; set; } = default!;
    public string RefreshToken { get; set; } = default! ;
}