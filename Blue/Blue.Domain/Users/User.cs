using Blue.Domain.Common;

namespace Blue.Domain.Users;

public class User : BaseEntity<Guid>
{
    public string Name { get; private set; } = default!;
    public string Email { get; private set; } = default!;
    public string PasswordHash { get; private set; } = default!;

    private User() { }

    private User(string name, string email, string passwordHash)
    {
        Id = Guid.NewGuid(); // Genera ID
        Name = name;
        Email = email;
        PasswordHash = passwordHash;
    }

    public static User Create(string name, string email, string passwordHash)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name is required");

        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email is required");

        if (string.IsNullOrWhiteSpace(passwordHash))
            throw new ArgumentException("Password is required");

        return new User(name, email, passwordHash);
    }
}