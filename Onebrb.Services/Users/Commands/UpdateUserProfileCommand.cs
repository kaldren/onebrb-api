using AutoMapper;
using MediatR;
using Onebrb.Application.Interfaces;
using Onebrb.Application.Users.Models;

namespace Onebrb.Application.Users.Commands;

public record UpdateUserProfileCommand : IRequest<UserProfileModel?>
{
    public UpdateUserProfileModel ProfileModel { get; set; }
}

internal class UpdateUserProfileHandler : IRequestHandler<UpdateUserProfileCommand, UserProfileModel?>
{
    private readonly IGenericRepository<Domain.Entities.Profile.Profile> _profileRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateUserProfileHandler(
        IGenericRepository<Domain.Entities.Profile.Profile> profileRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _profileRepository = profileRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<UserProfileModel?> Handle(UpdateUserProfileCommand request, CancellationToken cancellationToken)
    {
        // Find user
        var currentUser = await _profileRepository.GetSingleOrDefault(p => p.Id == request.ProfileModel.Id && p.Email == request.ProfileModel.Email);

        if (currentUser is null)
            return null;

        currentUser = _mapper.Map(request.ProfileModel, currentUser);

        _profileRepository.Update(currentUser);

        int result = await _unitOfWork.SaveChangesAsync();

        if (result > 0)
            return _mapper.Map<UserProfileModel>(currentUser);

        return null;
    }
}
