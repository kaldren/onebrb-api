using Microsoft.AspNetCore.Mvc;
using Onebrb.Core.Domain.Profile;

namespace Onebrb.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        public SearchController()
        {

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Profile>>> SearchProfilesAsync()
        {
            return new List<Profile>()
            {
                new Profile {FirstName = "John", LastName="Doe", Email = "john@example.com", About = "Number one plumber in Veliko Varnovo", Phone = "0888 123456", QRCode = "0f1fad5b-d9cb-479f-a165-70867728950e"},
                new Profile {FirstName = "Bob", LastName="Doe", Email = "bob@example.com", About = "Number one real estate broker in Veliko Varnovo", Phone = "0888 222222", QRCode = "0f8fad5b-d9cb-469f-a145-70867728920e"},
                new Profile {FirstName = "Steve", LastName="Doe", Email = "steve@example.com", About = "Number one taxi driver in Veliko Varnovo", Phone = "0888 333333", QRCode = "0f8fad5b-d3cb-463f-a165-70867728910e"},
                new Profile {FirstName = "Garry", LastName="Doe", Email = "garry@example.com", About = "Number one zumba trainer in Veliko Varnovo", Phone = "0888 444444", QRCode = "0f8fad5b-d9cb-469f-a165-70867228980e"},
                new Profile {FirstName = "Michael", LastName="Doe", Email = "michael@example.com", About = "Number one hair stylist in Veliko Varnovo", Phone = "0888 555555", QRCode = "0f2bbd5b-d9cb-469f-a165-70867728250e"}
            };
        }
    }
}
