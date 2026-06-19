# BlazorWindowsAuth

A .NET 6 solution that demonstrates a hybrid authentication approach combining Windows Authentication (Negotiate/Kerberos) and JWT Bearer tokens. It consists of a Web API backend and a Blazor WebAssembly client that communicates using hardcoded JWT credentials.

## Technologies

- .NET 6
- ASP.NET Core Web API
- Blazor WebAssembly
- Microsoft.AspNetCore.Authentication.Negotiate (Windows Auth)
- Microsoft.AspNetCore.Authentication.JwtBearer
- Swashbuckle / Swagger UI

## Key Features

- **Dynamic authentication scheme**: The API automatically selects between JWT Bearer (when `Authorization: Bearer` header is present) and Windows Negotiate authentication as a fallback.
- **CORS configuration**: Allows credentials from the Blazor client at `http://localhost:5244`.
- **Protected endpoints**: Both `/api/oauth` and `/api/test` require authorization.
- **Windows identity extraction**: The `/api/oauth` endpoint returns the authenticated user's Windows username.
- **Blazor client**: Calls the API using a pre-defined JWT token via `WindowsOAuthService`.
- **Swagger UI**: Available in development mode for testing.

## Solution Structure

```
BlazorWindowsAuth.API/       — Web API backend with authentication
BlazorWindowsAuth.Client/    — Blazor WebAssembly frontend
```

## How to Run

1. Start the API (requires IIS or Kestrel with Windows Authentication enabled):
   ```bash
   dotnet run --project BlazorWindowsAuth.API/BlazorWindowsAuth.API.csproj
   ```

2. Start the Blazor client:
   ```bash
   dotnet run --project BlazorWindowsAuth.Client/BlazorWindowsAuth.Client.csproj
   ```

3. Navigate to the client URL (`http://localhost:5244`).
