using ProductOverview.Domain.Products;

namespace ProductOverview.Application.Products;

public interface IProductRepository
{
    public Task<IReadOnlyCollection<Product>> GetAllAsync(CancellationToken token = default);
}