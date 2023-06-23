using System.Reflection;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using ProductOverview.Domain.Products;

namespace ProductOverview.Infrastructure.DbContexts;

public class ApplicationDbContextInitialiser
{
    private readonly ILogger<ApplicationDbContextInitialiser> _logger;

    private readonly ApplicationDbContext _context;

    public ApplicationDbContextInitialiser(
        ILogger<ApplicationDbContextInitialiser> logger,
        ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task SeedAsync(CancellationToken token)
    {
        try
        {
            await TrySeedAsync(token);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    public async Task TrySeedAsync(CancellationToken token)
    {
        var seedDataFile = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}/SeedData/products.json";
        if (!File.Exists(seedDataFile))
        {
            return;
        }

        var fileContent = await File.ReadAllTextAsync(seedDataFile, token);
        if (fileContent == null)
        {
            throw new IOException($"Something went wrong while reading the contents of the file at '{seedDataFile}'");
        }

        var products = JsonSerializer.Deserialize<IReadOnlyCollection<Product>>(fileContent)!;
        if (!_context.Products.Any())
        {
            await _context.Products.AddRangeAsync(products, token);

            await _context.SaveChangesAsync(token);
        }
    }
}
