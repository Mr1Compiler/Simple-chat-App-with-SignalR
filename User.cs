using Microsoft.AspNetCore.SignalR;
using System.Diagnostics.CodeAnalysis;

namespace SignalRTesting;
public class User  
{
	public string Id { get; set; }
	[AllowNull]
	public string Name { get; set; }
}
