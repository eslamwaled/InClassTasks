using Session28Api.Entities;

namespace Session28Api.Repositories;

public interface IUserRepository
{
    Task<User?> GetByEmailAsync(string email);

    Task<bool> ExistsByEmailAsync(string email);

    Task AddAsync(User user);

    Task SaveChangesAsync();
}
