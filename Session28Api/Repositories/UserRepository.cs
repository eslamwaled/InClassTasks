using Microsoft.EntityFrameworkCore;
using Session28Api.Data;
using Session28Api.Entities;

namespace Session28Api.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task<User?> GetByEmailAsync(string email) =>
        _context.Users.FirstOrDefaultAsync(user => user.Email == email);

    public Task<bool> ExistsByEmailAsync(string email) =>
        _context.Users.AnyAsync(user => user.Email == email);

    public async Task AddAsync(User user) =>
        await _context.Users.AddAsync(user);

    public Task SaveChangesAsync() =>
        _context.SaveChangesAsync();
}
