using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging; // Added for logging
using ModelContextProtocol.Server;
using System.ComponentModel;
using System.Threading; // Added for CancellationToken

var builder = WebApplication.CreateBuilder(args);

// Route logs to stderr so MCP hosts don't parse them as JSON-RPC
builder.Logging.AddConsole(o => o.LogToStandardErrorThreshold = LogLevel.Trace);

builder.Services
    .AddMcpServer()
    .WithToolsFromAssembly()     // reuse our attribute-based tools
    .WithPromptsFromAssembly();

var app = builder.Build();

// Exposes /messages (POST) and /sse (GET) endpoints for MCP Streamable HTTP
app.MapMcp();

app.Run();

// Example tool (same pattern as before)
[McpServerToolType]
public static class TimeTools
{
    [McpServerTool, Description("Gets the current server time in ISO-8601.")]
    public static string Now() => DateTimeOffset.UtcNow.ToString("O");
}