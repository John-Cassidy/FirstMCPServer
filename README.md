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

## Development Paths

1. **Minimal STDIO Server**: `MyMcpServer`

   - Create a console project and add the MCP SDK.
   - Define tools using attributes like `[McpServerTool]`.
   - Run the server locally using STDIO transport.

2. **Remote Server with ASP.NET Core**: `MyMcpSseServer`
   - Create a web app and add the MCP ASP.NET Core package.
   - Map MCP endpoints for HTTP/SSE transport.
   - Secure the server with authentication and rate limiting.

## Additional Features

- **Dependency Injection (DI)**: Inject services like `HttpClient` and `IMcpServer`.
- **Configuration & Secrets**: Use environment variables for keys and endpoints.
- **Observability**: Log to stderr and handle cancellation tokens.
- **Security**: Validate inputs, enforce least privilege, and secure secrets.

## Running MyMcpSseServer with Docker

This section provides step-by-step instructions for building and running the MyMcpSseServer locally using Docker.

### Prerequisites

- Docker Desktop installed and running on Windows 11 Pro (or equivalent)
- .NET 10 SDK installed
- VS Code with GitHub Copilot Chat

### Step 1: Build the Docker Image

From the root directory of the project, run:

```bash
docker build -t mymcpsseserver -f MyMcpSseServer/Dockerfile .
```

This builds the Docker image with the tag `mymcpsseserver`.

### Step 2: Run the Docker Container

Start the container with HTTP on port 8080:

```bash
docker run -d -p 8080:8080 --name mymcpsseserver mymcpsseserver
```

- `-d`: Runs the container in detached mode
- `-p 8080:8080`: Maps port 8080 on the host to port 8080 in the container
- `--name mymcpsseserver`: Names the container for easy reference

### Step 3: Verify the Container is Running

Check the container status:

```bash
docker ps
```

View the container logs:

```bash
docker logs mymcpsseserver
```

You should see output indicating the server is listening on `http://[::]:8080`.

### Step 4: Test with VS Code Copilot Chat

1. Open VS Code Copilot Chat (Ctrl+Shift+I)
2. Enable Agent Mode
3. Select `MyMcpSseServer` from the Tools picker
4. Test the tool:
   ```
   Using the Now tool, what is the current server time?
   ```

The server should respond with the current time in ISO-8601 format.

### Step 5: Monitor Container Logs

To watch logs in real-time:

```bash
docker logs -f mymcpsseserver
```

You should see entries like:

- `Server (MyMcpSseServer 1.0.0.0), Client (Visual Studio Code 1.107.1) method 'initialize' request handler called.`
- `Server (MyMcpSseServer 1.0.0.0), Client (Visual Studio Code 1.107.1) method 'tools/call' request handler completed.`

### Step 6: Stop and Remove the Container

To stop the container:

```bash
docker stop mymcpsseserver
```

To remove the container:

```bash
docker rm mymcpsseserver
```

To rebuild and restart:

```bash
docker build -t mymcpsseserver -f MyMcpSseServer/Dockerfile .
docker stop mymcpsseserver; docker rm mymcpsseserver
docker run -d -p 8080:8080 --name mymcpsseserver mymcpsseserver
```

### Configuration Notes

- The server uses HTTP (not HTTPS) for local development
- Port 8080 is configured via the `ASPNETCORE_URLS` environment variable in the Dockerfile
- The MCP configuration is located in `.vscode/mcp.json`
- No authentication token is required for local development

### Conclusion

MCP enables developers to build scalable and reusable AI tools with minimal effort. By following this guide, you can create a production-ready MCP server that integrates seamlessly with AI hosts like VS Code Copilot.

For more details, visit the [original article](https://blog.stackademic.com/build-mcp-server-dotnet-b4e1b19c8f38).
