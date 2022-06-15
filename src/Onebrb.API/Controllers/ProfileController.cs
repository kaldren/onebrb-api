using Microsoft.AspNetCore.Mvc;
using Onebrb.Core.Domain.Profile;
using Onebrb.Core.Interfaces;

namespace Onebrb.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileService _profileService;

        public ProfileController(
            IProfileService profileService
        )
        {
            this._profileService = profileService;
        }

        [HttpGet("{profileId}")]
        public async Task<ActionResult<Profile>> GetProfileAsync(int profileId)
        {
            var profile = await _profileService.GetProfileAsync(profileId);

            return null;
        }
    }
}
