using Microsoft.EntityFrameworkCore;
using ProductOverview.Domain.Products;

namespace ProductOverview.Infrastructure.DbContexts;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Product> Products => Set<Product>();
}