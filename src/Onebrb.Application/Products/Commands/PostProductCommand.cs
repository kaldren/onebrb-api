using AutoMapper;
using MediatR;
using Onebrb.Application.Interfaces;
using Onebrb.Application.Products.Models;
using Onebrb.Domain.Entities.ProductAggregate;

namespace Onebrb.Application.Products.Commands;

public class PostProductCommand : IRequest<ProductModel>
{
    public ProductInputModel Product { get; set; }
}

public class PostProductCommandHandler : IRequestHandler<PostProductCommand, ProductModel>
{
    private readonly IGenericRepository<Product> _productRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public PostProductCommandHandler(IGenericRepository<Product> productRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ProductModel> Handle(PostProductCommand request, CancellationToken cancellationToken)
    {
        Product product = _mapper.Map<Product>(request.Product);
        product.Id = Guid.NewGuid().ToString();
        product.ProfileId = request.Product.AuthorId;
        product.TrackId = Guid.NewGuid().ToString();

        _productRepository.Insert(product);

        await _unitOfWork.SaveChangesAsync();

        Product? updatedProduct = await _productRepository.GetSingleOrDefault(p => p.TrackId == product.TrackId);

        return _mapper.Map<ProductModel>(updatedProduct);
    }
}
