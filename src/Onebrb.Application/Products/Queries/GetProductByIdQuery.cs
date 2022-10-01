using AutoMapper;
using MediatR;
using Onebrb.Application.Interfaces;
using Onebrb.Application.Products.Models;
using Onebrb.Domain.Entities.ProductAggregate;

namespace Onebrb.Application.Products.Queries;

public class GetProductByIdQuery : IRequest<ProductModel>
{
    public string Id { get; set; }
}

public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductModel>
{
    private readonly IGenericRepository<Product> _productsRepository;
    private readonly IMapper _mapper;

    public GetProductByIdQueryHandler(IGenericRepository<Product> productsRepository, IMapper mapper)
    {
        _productsRepository = productsRepository;
        _mapper = mapper;
    }

    public async Task<ProductModel> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _productsRepository.GetAsync(request.Id);

        return _mapper.Map<ProductModel>(product);
    }
}