using Blue.Domain.Common;

namespace Blue.Domain.Users;

public class User : BaseEntity<Guid>
{
    public string Name { get; private set; } = default!;
    public string Email { get; private set; } = default!;
    public string PasswordHash { get; private set; } = default!;
    public UserRole Role { get; private set; }
    public string? RefreshToken { get; private set; }
    public DateTime? RefreshTokenExpiry { get; private set; }

    private User() { }

    private User(
        string name,
        string email,
        string passwordHash,
        UserRole role)
    {
        Id = Guid.NewGuid();
        Name = name;
        Email = email;
        PasswordHash = passwordHash;
        Role = role;
    }

    public static User Create(
        string name,
        string email,
        string passwordHash,
        UserRole role = UserRole.User)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name is required");

        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email is required");

        if (string.IsNullOrWhiteSpace(passwordHash))
            throw new ArgumentException("Password is required");

        return new User(name, email, passwordHash, role);
    }
    
    public static User CreateAdmin(string name, string email, string passwordHash)
    {
        return new User(name, email, passwordHash, UserRole.Admin);
    }
    
    public void SetRefreshToken(string token, DateTime expires)
    {
        RefreshToken = token;
        RefreshTokenExpiry = expires;
    }
}