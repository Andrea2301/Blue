using Blue.Application.Auth.DTOs;
using Blue.Application.Common.Interfaces;
using Blue.Domain.Users;

namespace Blue.Application.Auth.Commands;

public class LoginUserCommand
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ITokenGenerator _tokenGenerator;

    public LoginUserCommand(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        ITokenGenerator tokenGenerator)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _tokenGenerator = tokenGenerator;
    }

    public async Task<LoginResponse> ExecuteAsync(LoginRequest request)
    {
        // buscar usuario
        var user = await _userRepository.GetUserByEmail(request.Email);
        if (user is null)
            throw new InvalidOperationException("Invalid credentials");

        // Verificar password
        var isValid = _passwordHasher.Verify(
            request.Password,
            user.PasswordHash
        );

        if (!isValid)
            throw new InvalidOperationException("Invalid credentials");

        // Generar token
        var token = _tokenGenerator.GenerateAccessToken(user);

        //  Retornar resultado
        return new LoginResponse
        {
            UserId = user.Id,
            Name = user.Name,
            Email = user.Email,
            Token = token
        };
    }
}