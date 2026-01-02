using Microsoft.Extensions.AI;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ModelContextProtocol.Server;
using System.ComponentModel;

var builder = Host.CreateApplicationBuilder(args);

// Route logs to stderr so MCP hosts don't parse them as JSON-RPC
builder.Logging.AddConsole(o => o.LogToStandardErrorThreshold = LogLevel.Trace);

builder.Services
    .AddMcpServer()
    .WithStdioServerTransport()  // run as a local process via stdio
    .WithToolsFromAssembly()     // discover [McpServerTool] in this assembly
    .WithPromptsFromAssembly();  // discover [McpServerPrompt]

await builder.Build().RunAsync();

// ===== Tools =====
[McpServerToolType]
public static class EchoTools
{
    [McpServerTool, Description("Echoes the message back to the client.")]
    public static string Echo([Description("Message to echo")] string message)
        => $"Hello from .NET: {message}";

    [McpServerTool(Name = "reverse_echo"), Description("Returns the reversed message.")]
    public static string ReverseEcho([Description("Message to reverse")] string message)
        => new string(message.Reverse().ToArray());
}

// ===== Prompts =====
[McpServerPromptType]
public static class DemoPrompts
{
    [McpServerPrompt, Description("Create a succinct summary prompt for given text.")]
    public static ChatMessage Summarize([Description("Text to summarize")] string content)
        => new(ChatRole.User, $"Please summarize in one sentence: {content}");
}