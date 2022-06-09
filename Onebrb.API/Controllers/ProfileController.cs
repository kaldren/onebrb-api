using Microsoft.AspNetCore.Mvc;
using Onebrb.API.Services.Interfaces;
using Onebrb.API.Services.Models;

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
        public async Task<ActionResult<ProfileModel>> GetProfileAsync(int profileId)
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
