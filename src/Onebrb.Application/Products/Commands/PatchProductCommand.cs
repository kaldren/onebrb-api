using AutoMapper;
using MediatR;
using Onebrb.Application.Enums;
using Onebrb.Application.Interfaces;
using Onebrb.Application.Products.Models;
using Onebrb.Domain.Entities.ProductAggregate;

namespace Onebrb.Application.Products.Commands;

public class PatchProductCommand : IRequest<HandlerResponse<ProductModel>>
{
    public ProductInputModel Product { get; set; }
}

public class PatchProductCommandHandler : IRequestHandler<PatchProductCommand, HandlerResponse<ProductModel>>
{
    private readonly IGenericRepository<Product> _productRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public PatchProductCommandHandler(IGenericRepository<Product> productRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<HandlerResponse<ProductModel>> Handle(PatchProductCommand request, CancellationToken cancellationToken)
    {
        HandlerResponse<ProductModel> response = new();

        // Get the product
        var productToUpdate = await _productRepository.GetAsync(request.Product.Id);

        if (productToUpdate?.Profile.Id != request.Product.AuthorId)
        {
            response.Message = "The product doesn't belong to this user.";

            return response;
        }

        productToUpdate = _mapper.Map(request.Product, productToUpdate);
        //productToUpdate.TrackId = Guid.NewGuid().ToString();

        _productRepository.Update(productToUpdate);

        int rowsUpdated = await _unitOfWork.SaveChangesAsync();

        if (rowsUpdated > 0)
        {
            //Product? updatedProduct = await _productRepository.GetSingleOrDefault(p => p.TrackId == productToUpdate.TrackId);

            response.Status = HandlerStatus.Success;
            response.Result = _mapper.Map<ProductModel>(productToUpdate);
        }

        return response;
    }
}
