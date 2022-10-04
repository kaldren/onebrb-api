using MediatR;
using Onebrb.Application.Enums;
using Onebrb.Application.Interfaces;
using Onebrb.Domain.Entities.ProductAggregate;

namespace Onebrb.Application.Products.Commands;

public class DeleteProductByIdCommand : IRequest<HandlerResponse>
{
    public string ProductId { get; set; }

    public string CurrentUserId { get; set; }
}

internal class DeleteProductByIdCommandHandler : IRequestHandler<DeleteProductByIdCommand, HandlerResponse>
{
    private readonly IGenericRepository<Product> _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteProductByIdCommandHandler(IGenericRepository<Product> productRepository, IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<HandlerResponse> Handle(DeleteProductByIdCommand request, CancellationToken cancellationToken)
    {
        HandlerResponse response = new();

        Product? product = await _productRepository.GetAsync(request.ProductId);

        if (product is null)
        {
            response.Status = HandlerStatus.Failure;
            response.Message = "Product doesn't exist.";
        }

        else if (product.Profile.Id != request.CurrentUserId)
        {
            response.Status = HandlerStatus.Failure;
            response.Message = "Product is not owned by the current user.";
        }

        else
        {
            product.IsDeleted = true;

            _productRepository.Update(product);

            int result = await _unitOfWork.SaveChangesAsync();

            if (result > 0)
                response.Status = HandlerStatus.Success;
        }

        return response;
    }
}