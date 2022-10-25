using MediatR;
using Onebrb.Application.Interfaces;
using Onebrb.Application.Notifications.Models;
using Onebrb.Domain.Entities.ProfileAggregate;

namespace Onebrb.Application.Notifications.Commands.InterestedInProductNotification;

public class InterestedInProductNotificationCommand : IRequest<bool>
{
    public InterestedInProductNotificationModel Model { get; set; }
}

public class InterestedInProductNotificationCommandHandler : IRequestHandler<InterestedInProductNotificationCommand, bool>
{
    private readonly IGenericRepository<Comment> _commentRepository;
    private readonly IUnitOfWork _unitOfWork;

    public InterestedInProductNotificationCommandHandler(IGenericRepository<Comment> commentRepository, IUnitOfWork unitOfWork)
    {
        _commentRepository = commentRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(InterestedInProductNotificationCommand request, CancellationToken cancellationToken)
    {
        return false;
    }
}

