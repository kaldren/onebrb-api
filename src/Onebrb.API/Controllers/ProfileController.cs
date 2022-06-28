using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Onebrb.Core.Domain.Profile;
using Onebrb.Core.Interfaces;

namespace Onebrb.API.Controllers
{
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
    }
}
