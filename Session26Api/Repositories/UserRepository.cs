using Microsoft.EntityFrameworkCore;
using Session26Api.Data;
using Session26Api.Entities;

namespace Session26Api.Repositories;

public class UserRepository(AppDbContext dbContext) : IUserRepository
{
    public Task<User?> GetByEmailAsync(string email)
        => dbContext.Users.SingleOrDefaultAsync(u => u.Email == email);

    public Task<bool> ExistsByEmailAsync(string email)
        => dbContext.Users.AnyAsync(u => u.Email == email);

    public async Task AddAsync(User user)
        => await dbContext.Users.AddAsync(user);

    public Task SaveChangesAsync()
        => dbContext.SaveChangesAsync();
}
