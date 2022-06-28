using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using Onebrb.Core.Domain.Profile;
using Onebrb.Core.Interfaces;
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

        public ProfilesController(
            IProfileService profileService
        )
        {
            this._profileService = profileService;
        }

        [HttpGet("{profileId}")]
        public async Task<ActionResult<Profile>> GetProfileAsync(int profileId)
        {
            var profile = await _profileService.GetProfileAsync(profileId);

            if (profile == null) return NotFound();

            return Ok(profile);
        }

        [HttpGet]
        public async Task<ActionResult<Profile>> GetCurrentUserProfile()
        {
            string? currentUserEmail = User?.Claims?.FirstOrDefault(x => x.Type == "emails")?.Value;

            if (currentUserEmail is null)
                return Unauthorized();

            var profile = await _profileService.GetProfileAsync(currentUserEmail);

            if (profile == null)
                return NotFound();

            return Ok(profile);
        }

        [HttpPatch]
        public async Task<ActionResult> EditProfileAsync([FromBody][Required] Profile profileModel)
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
