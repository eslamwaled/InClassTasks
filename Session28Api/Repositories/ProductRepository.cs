using Microsoft.EntityFrameworkCore;
using Session28Api.Data;
using Session28Api.Entities;

namespace Session28Api.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task<List<Product>> GetAllAsync() =>
        _context.Products.AsNoTracking().ToListAsync();

    public Task<Product?> GetByIdAsync(int id) =>
        _context.Products.FirstOrDefaultAsync(product => product.Id == id);

    public async Task AddAsync(Product product) =>
        await _context.Products.AddAsync(product);

    public Task DeleteAsync(Product product)
    {
        _context.Products.Remove(product);
        return Task.CompletedTask;
    }

    public Task SaveChangesAsync() =>
        _context.SaveChangesAsync();
}
