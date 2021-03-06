using MediatR;
using Onebrb.Application.Interfaces;
using Onebrb.Application.Users.Models;
using Onebrb.Domain.Entities.Profile;

namespace Onebrb.Application.Users.Commands;

public record ActivateUserCommand : IRequest<UserProfileModel>
{
    public ActivateUserProfileModel ProfileModel { get; set; }
}

internal class ActivateUserProfileHandler : IRequestHandler<ActivateUserCommand, UserProfileModel>
{
    private readonly IGenericRepository<Profile> _profileRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ActivateUserProfileHandler(IGenericRepository<Profile> profileRepository, IUnitOfWork unitOfWork)
    {
        _profileRepository = profileRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<UserProfileModel> Handle(ActivateUserCommand request, CancellationToken cancellationToken)
    {
        _profileRepository.Insert(new Profile
        {
            Id = request.ProfileModel.Id,
            Email = request.ProfileModel.Email,
            FirstName = request.ProfileModel.Name.Split(' ')[0],
            LastName = request.ProfileModel.Name.Split(' ')[1]
        });

        int result = await _unitOfWork.SaveChangesAsync();

        if (result > 0)
            return new UserProfileModel
            {
                Email = request.ProfileModel.Email,
                Id = request.ProfileModel.Id,
                FirstName = request.ProfileModel.Name.Split(' ')[0],
                LastName = request.ProfileModel.Name.Split(' ')[1]
            };

        return null;
    }
}
