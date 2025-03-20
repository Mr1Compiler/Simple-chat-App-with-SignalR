using Microsoft.AspNetCore.SignalR;
using SignalRTesting;

var builder = WebApplication.CreateBuilder(args);

// SignalR service Options => -_-
builder.Services.AddSignalR(options =>
{
	options.EnableDetailedErrors = true;
});

builder.Services.AddControllers();
builder.Services.AddSingleton<IUserIdProvider, CustomUserIdProvider>();


var app = builder.Build();

// Configure middleware
app.UseRouting();
app.UseStaticFiles();

// Map endpoints for controllers and SignalR hubs
app.MapControllers();
app.MapHub<ChatHub>("/chat");


app.Run();

