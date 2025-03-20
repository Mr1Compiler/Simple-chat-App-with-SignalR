using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.SignalR;

public class ChatHub : Hub
{
	private static Dictionary<string, string> connectedUsers = new();

	public override async Task OnConnectedAsync()
	{
		var httpContext = Context.GetHttpContext();
		var userId = httpContext.Request.Query["userId"]; // Taking the userId from the url.
		var userName = httpContext.Request.Query["userName"]; // Taking the userName

		// Displaying all the URIs here -_-
		var all = httpContext.Request.GetDisplayUrl(); 

		if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(userName))
		{
			//			    key	   =       Value
			connectedUsers[userId!] = Context.ConnectionId;
			await Clients.All.SendAsync("SendNewUserConnected", userId, userName);
		}

		await base.OnConnectedAsync();
	}

	public override async Task OnDisconnectedAsync(Exception? exception)
	{
		var userId = connectedUsers.FirstOrDefault(x => x.Value == Context.ConnectionId).Key;
		if (!string.IsNullOrEmpty(userId))
		{
			connectedUsers.Remove(userId);
		}
		await base.OnDisconnectedAsync(exception);
	}

	public Task RegisterUser(string id, string name)
	{
		connectedUsers[id] = Context.ConnectionId;
		return Clients.All.SendAsync("SendNewUserConnected", id, name);
	}

	public async Task SendToUser(string sender, string receiver, string message)
	{
		if (connectedUsers.TryGetValue(receiver, out var connectionId))
		{
			await Clients.Client(connectionId).SendAsync("SendToUser", sender, receiver, message);
		}
	}
}


