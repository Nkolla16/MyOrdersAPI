using Microsoft.AspNetCore.Mvc;
using MyOrdersAPI.Models;
using MyOrdersAPI.Manager.IManagers;
using System.Threading.Tasks;

namespace MyOrdersAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PushNotificationController : ControllerBase
    {
        private readonly IPushNotificationManager _manager;

        public PushNotificationController(IPushNotificationManager manager)
        {
            _manager = manager;
        }

        [HttpPost]
        [Route("registertoken")]
        public async Task<IActionResult> RegisterToken([FromBody] PushNotificationTokenModel model)
        {
            if (!ModelState.IsValid || string.IsNullOrWhiteSpace(model.Token))
                return BadRequest("Token is required.");

            var success = await _manager.RegisterTokenAsync(model.Token);
            if (!success)
                return StatusCode(500, "Failed to register push notification token.");

            return Ok("Token registered successfully.");
        }
    }
}
