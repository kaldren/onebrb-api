using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
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
        [EnableQuery]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<IEnumerable<Profile>>> SearchProfilesAsync()
        {
            return new List<Profile>()
            {
                new Profile {Id = 1, FirstName = "John", LastName="Doe", Email = "john@example.com", About = "Number one plumber in Veliko Varnovo", Phone = "0888 123456"},
                new Profile {Id = 2, FirstName = "John", LastName="Boe", Email = "johnb@example.com", About = "Number two plumber in Veliko Varnovo", Phone = "0888 222456"},
                new Profile {Id = 2, FirstName = "John", LastName="Soe", Email = "johns@example.com", About = "Number three plumber in Veliko Varnovo", Phone = "0888 333456"},
                new Profile {Id = 2, FirstName = "Bob", LastName="Doe", Email = "bob@example.com", About = "Number one real estate broker in Veliko Varnovo", Phone = "0888 222222"},
                new Profile {Id = 2, FirstName = "Steve", LastName="Doe", Email = "steve@example.com", About = "Number one taxi driver in Veliko Varnovo", Phone = "0888 333333"},
                new Profile {Id = 2, FirstName = "Garry", LastName="Doe", Email = "garry@example.com", About = "Number one zumba trainer in Veliko Varnovo", Phone = "0888 444444"},
                new Profile {Id = 2, FirstName = "Michael", LastName="Doe", Email = "michael@example.com", About = "Number one hair stylist in Veliko Varnovo", Phone = "0888 555555"}
            };
        }
    }
}
