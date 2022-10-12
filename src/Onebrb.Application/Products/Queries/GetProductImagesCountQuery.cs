using MediatR;
using Onebrb.Application.Interfaces;
using Onebrb.Domain.Entities.ProductAggregate;

namespace Onebrb.Application.Products.Queries;

public class GetProductImagesCountQuery : IRequest<int>
{
    public string Id { get; set; }
}

public class GetProductImagesCountQueryHandler : IRequestHandler<GetProductImagesCountQuery, int>
{
    private readonly IGenericRepository<ProductImage> _productImagesRepository;

    public GetProductImagesCountQueryHandler(IGenericRepository<ProductImage> productImagesRepository)
    {
        _productImagesRepository = productImagesRepository;
    }

    public async Task<int> Handle(GetProductImagesCountQuery request, CancellationToken cancellationToken)
    {
        return (await _productImagesRepository.GetAsync(p => p.ProductId == request.Id)).Count;
    }
}