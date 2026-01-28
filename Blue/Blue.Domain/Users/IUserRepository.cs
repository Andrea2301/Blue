namespace Blue.Domain.Users;

public interface IUserRepository
{
    Task<User?> GetUserByEmail(string email);
    Task AddAsync(User user);
    Task<User?> GetByRefreshTokenAsync(string refreshToken);
    Task UpdateAsync(User user);

}