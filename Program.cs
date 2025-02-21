using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.SignalR;
using SignalRTesting;

var builder = WebApplication.CreateBuilder(args);

// SignalR service Options => -_-
builder.Services.AddSignalR(options =>
{
	options.EnableDetailedErrors = true;
});

builder.Services.AddControllers();

var app = builder.Build();

// Configure middleware
app.UseRouting();
app.UseStaticFiles();

app.Use(async (context, next) =>
{
	// Check if this request is for the SignalR negotiate endpoint
	if (context.Request.Path.StartsWithSegments("/chat/negotiate"))
	{
		if (!context.Request.Cookies.TryGetValue("DeviceId", out var deviceId))
		{
			deviceId = Guid.NewGuid().ToString();
			context.Response.Cookies.Append("DeviceId", deviceId, new CookieOptions
			{
				Expires = DateTimeOffset.UtcNow.AddMinutes(1)
			});

			Console.WriteLine("Cookie set in middleware: " + deviceId);
		}
	}
	await next();
});

// Map endpoints for controllers and SignalR hubs
app.MapControllers();
app.MapHub<ChatHub>("/chat");


app.Run();

