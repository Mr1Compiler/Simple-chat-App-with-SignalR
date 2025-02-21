using System.Diagnostics.CodeAnalysis;

namespace SignalRTesting;
public class User  
{
	public int Id { get; set; }
	[AllowNull]
	public string Name { get; set; } 
}
