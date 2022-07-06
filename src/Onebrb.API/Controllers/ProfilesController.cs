using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using Onebrb.API.Models;
using Onebrb.Services.Profiles;
using System.ComponentModel.DataAnnotations;

namespace Onebrb.API.Controllers
{
    [RequiredScope("Onebrb.Read")]
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProfilesController : ControllerBase
    {
        private readonly IProfileService _profileService;
        private readonly IMapper _mapper;

        public ProfilesController(
            IProfileService profileService,
            IMapper mapper
        )
        {
            this._profileService = profileService;
            _mapper = mapper;
        }

        [HttpGet("{profileId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Core.Domain.Profile.Profile>> GetProfileAsync(int profileId)
        {
            var profile = await _profileService.GetProfileAsync(profileId);

            if (profile == null) return NotFound();

            return Ok(profile);
        }

        [HttpGet("current-profile")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Core.Domain.Profile.Profile>> GetCurrentUserProfileAsync()
        {
            string? currentUserEmail = User?.Claims?.FirstOrDefault(x => x.Type == "emails")?.Value;

            if (currentUserEmail is null)
                return Unauthorized();

            var profile = await _profileService.GetProfileAsync(currentUserEmail);

            if (profile == null)
                return NotFound();

            return Ok(profile);
        }

        [HttpPost("activate-profile")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ActivatedProfileResponseModel>> ActivateProfileAsync()
        {
            string? currentUserEmail = User?.Claims?.FirstOrDefault(x => x.Type == "emails")?.Value;
            string? currentUserName = User?.Claims?.FirstOrDefault(x => x.Type == "name")?.Value;

            var profile = await _profileService.ActivateProfile();

            return Ok(_mapper.Map<ActivatedProfileResponseModel>(profile));
        }

        [HttpPatch]
        public async Task<ActionResult> EditProfileAsync([FromBody][Required] Core.Domain.Profile.Profile profileModel)
        {
            string? currentUserEmail = User?.Claims?.FirstOrDefault(x => x.Type == "emails")?.Value;

            if (currentUserEmail is null || profileModel?.Email != currentUserEmail)
                return Unauthorized();

            var profile = await _profileService.UpdateProfileAsync(profileModel);

            if (profile == null)
                return NotFound();

            return Ok(profile);
        }
    }
}
