using Microsoft.EntityFrameworkCore;
using ProductOverview.Domain.Products;

namespace ProductOverview.Infrastructure.DbContexts;

public interface IApplicationDbContext
{
    DbSet<Product> Products { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}