using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace SignalRTesting
{
	public class NotificationsController : Controller
	{
		private readonly IHubContext<ChatHub> _hubContext;

		public NotificationsController(IHubContext<ChatHub> hubContext)
		{
			_hubContext = hubContext;
		}

		public async Task<IActionResult> SendNotification(string message)
		{
			await _hubContext.Clients.All.SendAsync("ReceiveMessage", message);
			return Ok("Notification sent!");
		}
	}
}
