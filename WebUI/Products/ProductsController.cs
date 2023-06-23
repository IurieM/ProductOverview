using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductOverview.Application.Products.Queries;
using Swashbuckle.AspNetCore.Annotations;

namespace WebUI.Products;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [SwaggerResponse(StatusCodes.Status200OK, "Get rooms with the provider space identifiers", typeof(IReadOnlyCollection<ProductDto>))]
    [SwaggerOperation(Summary = "Get rooms", Description = "Gets rooms with the provided space identifiers", OperationId = nameof(GetAllAsync))]
    public async Task<IActionResult> GetAllAsync([FromQuery] GetProductsQuery query)
    {
        var products = await _mediator.Send(query);
        return Ok(products);
    }
}

