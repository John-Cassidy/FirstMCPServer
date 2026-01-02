# FirstMCPServer

Build an MCP Server in .NET

## Overview: Building an MCP Server in .NET

This guide provides a comprehensive walkthrough for creating a Model Context Protocol (MCP) server using .NET. MCP is a standardized protocol that enables AI applications to interact with tools, resources, and prompts over well-defined transports like STDIO or HTTP/SSE. Below is a summary of the key steps and concepts covered in the guide:

### Why MCP?

MCP simplifies the integration of AI applications with external tools by providing a reusable and scalable interface. It supports multiple clients and hosts, making it ideal for applications like VS Code Copilot Chat and other AI-driven tools.

### Prerequisites

- .NET 8+ (or .NET 10 preview for the latest features).
- Familiarity with C# and ASP.NET Core.
- VS Code with GitHub Copilot Chat (Agent Mode) or any MCP-capable host.

### Key Concepts

- **Server**: Exposes tools, resources, and prompts.
- **Client**: Connects to the server.
- **Host**: Owns the client (e.g., VS Code Copilot).
- **Transports**: STDIO for local development and HTTP/SSE for remote servers.

### Development Paths

1. **Minimal STDIO Server**:

   - Create a console project and add the MCP SDK.
   - Define tools using attributes like `[McpServerTool]`.
   - Run the server locally using STDIO transport.

2. **Remote Server with ASP.NET Core**:
   - Create a web app and add the MCP ASP.NET Core package.
   - Map MCP endpoints for HTTP/SSE transport.
   - Secure the server with authentication and rate limiting.

### Additional Features

- **Dependency Injection (DI)**: Inject services like `HttpClient` and `IMcpServer`.
- **Configuration & Secrets**: Use environment variables for keys and endpoints.
- **Observability**: Log to stderr and handle cancellation tokens.
- **Security**: Validate inputs, enforce least privilege, and secure secrets.

### Conclusion

MCP enables developers to build scalable and reusable AI tools with minimal effort. By following this guide, you can create a production-ready MCP server that integrates seamlessly with AI hosts like VS Code Copilot.

For more details, visit the [original article](https://blog.stackademic.com/build-mcp-server-dotnet-b4e1b19c8f38).
