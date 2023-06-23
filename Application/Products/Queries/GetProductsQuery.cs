using AutoMapper;
using MediatR;

namespace ProductOverview.Application.Products.Queries;

public class GetProductsQuery : IRequest<IReadOnlyCollection<ProductDto>>
{
}

public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, IReadOnlyCollection<ProductDto>>
{
    private readonly IMapper _mapper;

    private readonly IProductRepository _repository;

    public GetProductsQueryHandler(IMapper mapper, IProductRepository repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<IReadOnlyCollection<ProductDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await _repository.GetAllAsync(cancellationToken);
        return _mapper.Map<IReadOnlyCollection<ProductDto>>(products);
    }
}

