using Microsoft.AspNetCore.SignalR;

namespace SignalRTesting
{
	public class CustomUserIdProvider : IUserIdProvider
	{
		public string? GetUserId(HubConnectionContext connection)
		{
			return connection.GetHttpContext()?.Request.Cookies["DeviceId"];
		}
	}
}
