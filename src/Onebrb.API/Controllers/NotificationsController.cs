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
        private readonly INotificationsService _notificationsService;

        public NotificationsController(IAzureQueueService azureQueueService, INotificationsService notificationsService)
        {
            _azureQueueService = azureQueueService;
            _notificationsService = notificationsService;
        }

        //[HttpGet]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status401Unauthorized)]
        //public async Task<ActionResult> Notify()
        //{
        //    await _notificationsService.Notify();

        //    return Ok();
        //}

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> Notify([FromQuery] string connectionId)
        {
            //NotificationsHub
            await _notificationsService.Notify(connectionId);

            return Ok();
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
