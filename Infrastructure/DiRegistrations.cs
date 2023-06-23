using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProductOverview.Application.Products;
using ProductOverview.Infrastructure.DbContexts;
using ProductOverview.Infrastructure.Products;

namespace ProductOverview.Infrastructure;

public static class DiRegistrations
{
    public static IServiceCollection RegisterInfrastructureServices(this IServiceCollection services)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseInMemoryDatabase("ProductOverviewDb"));

        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
        services.AddScoped<ApplicationDbContextInitialiser>();

        services.AddTransient<IProductRepository, ProductRepository>();

        return services;
    }
}
