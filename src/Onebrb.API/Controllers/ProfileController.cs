using Microsoft.AspNetCore.Mvc;
using Onebrb.Core.Domain;
using Onebrb.Core.Interfaces;

namespace Onebrb.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileService profileService;

        public ProfileController(IProfileService profileService)
        {
            this.profileService = profileService;
        }

        [HttpGet("{profileId}")]
        public async Task<ActionResult<Profile>> GetProfileAsync(int profileId)
        {
            try
            {
                var profile = await profileService.GetProfileAsync(profileId);
            }
            catch (Exception ex)
            {

                throw;
            }

            return null;
        }
    }
}
