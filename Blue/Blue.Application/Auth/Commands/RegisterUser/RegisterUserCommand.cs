using Blue.Application.Auth.DTOs;
using Blue.Application.Common.Interfaces;
using Blue.Domain.Users;

namespace Blue.Application.Auth.Commands.RegisterUser;

public class RegisterUserCommand
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    
    public RegisterUserCommand(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }
    
    public async Task ExecuteAsync(RegisterRequest request)
    {
        // Validar unicidad
        var existingUser = await _userRepository.GetUserByEmail(request.Email);
        if (existingUser is not null)
            throw new InvalidOperationException("User already exists");

       
        var passwordHash = _passwordHasher.Hash(request.Password);

        
        var user = User.Create(
            request.Name,
            request.Email,
            passwordHash
        );

      
        await _userRepository.AddAsync(user);
    }

}