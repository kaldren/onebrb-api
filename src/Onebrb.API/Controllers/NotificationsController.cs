using Microsoft.AspNetCore.Mvc;
using Onebrb.Infrastructure.Interfaces;

namespace Onebrb.API.Controllers
{
    //[RequiredScope("Onebrb.Read")]
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly IAzureQueueService _azureQueueService;

        public NotificationsController(IAzureQueueService azureQueueService)
        {
            _azureQueueService = azureQueueService;
        }

        #region PUT
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<bool>> InsertAsync()
        {
            return await _azureQueueService.InsertAsync("notifications", "testkalata");
        }
        #endregion PUT
    }
}
