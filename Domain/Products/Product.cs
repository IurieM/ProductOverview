namespace ProductOverview.Domain.Products;

public class Product
{
    public int ProductId { get; set; }

    public string ProductName { get; set; } = default!;

    public string? ServiceName { get; set; }

    public decimal? CreditVolume { get; set; }

    public decimal? DebitVolume { get; set; }
}
