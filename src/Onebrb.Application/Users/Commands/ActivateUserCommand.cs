using MediatR;
using Onebrb.Application.Interfaces;
using Onebrb.Application.Users.Models;
using Onebrb.Domain.Entities.ProfileAggregate;

namespace Onebrb.Application.Users.Commands;

public record ActivateUserCommand : IRequest<UserProfileModel>
{
    public ActivateUserProfileModel ProfileModel { get; set; }
}

internal class ActivateUserProfileHandler : IRequestHandler<ActivateUserCommand, UserProfileModel>
{
    private readonly IGenericRepository<ProfileEntity> _profileRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ActivateUserProfileHandler(IGenericRepository<ProfileEntity> profileRepository, IUnitOfWork unitOfWork)
    {
        _profileRepository = profileRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<UserProfileModel> Handle(ActivateUserCommand request, CancellationToken cancellationToken)
    {
        _profileRepository.Insert(new ProfileEntity
        {
            Id = request.ProfileModel.Id,
            Email = request.ProfileModel.Email,
            FirstName = request.ProfileModel.FirstName,
            LastName = request.ProfileModel.LastName
        });

        int result = await _unitOfWork.SaveChangesAsync();

        if (result > 0)
            return new UserProfileModel
            {
                Email = request.ProfileModel.Email,
                Id = request.ProfileModel.Id,
                FirstName = request.ProfileModel.FirstName,
                LastName = request.ProfileModel.LastName
            };

        return null;
    }
}
