using Microsoft.EntityFrameworkCore;
using ProductOverview.Application.Products;
using ProductOverview.Domain.Products;
using ProductOverview.Infrastructure.DbContexts;

namespace ProductOverview.Infrastructure.Products;

internal class ProductRepository : IProductRepository
{
    private readonly IApplicationDbContext _dbContext;

    public ProductRepository(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyCollection<Product>> GetAllAsync(CancellationToken token = default)
    {
        return await _dbContext.Products.ToListAsync(token);
    }
}
