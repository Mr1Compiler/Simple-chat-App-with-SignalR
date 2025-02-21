using Microsoft.AspNetCore.SignalR;
namespace SignalRTesting;

public class ChatHub : Hub
{
	public override async Task OnConnectedAsync()
	{
		HttpContext httpContext = Context.GetHttpContext()!;
		string deviceId;

		if (httpContext.Request.Cookies.TryGetValue("DeviceId", out deviceId!))
		{
			await Clients.Caller.SendAsync("ReceiveDeviceId", deviceId);
			Console.WriteLine($"Existing Device ID: {deviceId}");
		}
		else
		{
			Console.WriteLine("There is an error getting the Device Id");
		}
		// Send the Device ID back to the client
		await base.OnConnectedAsync();
	}


	public async Task SendMessage(string deviceId, string message)
	{
		HttpContext httpContext = Context.GetHttpContext()!;

		if (httpContext.Request.Cookies.TryGetValue("DeviceId", out deviceId))
			await Clients.All.SendAsync("SendMessage", deviceId, message);

	}
}


