using AutoMapper;
using MediatR;
using Onebrb.Application.Interfaces;
using Onebrb.Application.Products.Models;
using Onebrb.Domain.Entities.ProductAggregate;

namespace Onebrb.Application.Products.Queries;

public class GetAllProductsByUserIdQuery : IRequest<ICollection<ProductModel>>
{
    public string Id { get; set; }
}

public class GetAllProductsByUserIdQueryHandler : IRequestHandler<GetAllProductsByUserIdQuery, ICollection<ProductModel>>
{
    private readonly IGenericRepository<Product> _productsRepository;
    private readonly IMapper _mapper;

    public GetAllProductsByUserIdQueryHandler(IGenericRepository<Product> productsRepository, IMapper mapper)
    {
        _productsRepository = productsRepository;
        _mapper = mapper;
    }

    public async Task<ICollection<ProductModel>> Handle(GetAllProductsByUserIdQuery request, CancellationToken cancellationToken)
    {
        var products = await _productsRepository.GetAsync(p => p.Profile.Id == request.Id && p.IsDeleted == false);

        return _mapper.Map<ICollection<ProductModel>>(products);
    }
}