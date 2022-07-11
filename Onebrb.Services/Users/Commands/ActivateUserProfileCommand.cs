using AutoMapper;
using MediatR;
using Onebrb.Application.Users.Models;
using Onebrb.Domain.Interfaces;

namespace Onebrb.Application.Users.Commands;

public record ActivateUserProfileCommand : IRequest<UserProfileModel>
{
    public ActivateUserProfileModel ProfileModel { get; set; }
}

internal class ActivateUserProfileHandler : IRequestHandler<ActivateUserProfileCommand, UserProfileModel>
{
    private readonly IGenericRepository<Domain.Entities.Profile.Profile> _profileRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ActivateUserProfileHandler(IGenericRepository<Domain.Entities.Profile.Profile> profileRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _profileRepository = profileRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<UserProfileModel> Handle(ActivateUserProfileCommand request, CancellationToken cancellationToken)
    {
        _profileRepository.Insert(new Domain.Entities.Profile.Profile
        {
            Email = request.ProfileModel.Email,
            ProfileId = request.ProfileModel.Id,
            FirstName = request.ProfileModel.Name.Split(' ')[0],
            LastName = request.ProfileModel.Name.Split(' ')[1]
        });

        int result = await _unitOfWork.SaveChangesAsync();

        if (result > 0)
            return new UserProfileModel
            {
                Email = request.ProfileModel.Email,
                ProfileId = request.ProfileModel.Id,
                FirstName = request.ProfileModel.Name.Split(' ')[0],
                LastName = request.ProfileModel.Name.Split(' ')[1]
            };

        return null;
    }
}
