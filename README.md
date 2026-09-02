# BlazorWindowsAuth

A demo of **hybrid authentication** combining **Windows authentication** (Negotiate/Kerberos) with **JWT bearer tokens** in an ASP.NET Core Web API (`.NET 6`) consumed by a Blazor WebAssembly client.

## Architecture

A simple two-project setup:

| Project | Responsibility |
|---|---|
| `BlazorWindowsAuth.API` | Web API that configures `Negotiate` + `JwtBearer` and a **policy scheme** (`DynamicScheme`) that switches authentication based on the request's `Authorization` header (Bearer token if present, otherwise Windows/Negotiate). |
| `BlazorWindowsAuth.Client` | Blazor WebAssembly client that calls the API with a JWT and exercises the authenticated endpoints. |

## Authentication flow

- Requests with an `Authorization: Bearer <jwt>` header are authenticated with **JWT**.
- Requests without a bearer header fall back to **Windows (Negotiate)** authentication.
- CORS is enabled for the client origin.

## Endpoints

- `GET /api/oauth` – returns the authenticated username (Windows auth).
- `GET /api/test` – JWT-protected test endpoint (requires a bearer token).

## Getting started

```bash
dotnet restore
dotnet build BlazorWindowsAuth.sln
```

Run the API, then run the Blazor WebAssembly client, which points at `http://localhost:5187/api/`.

## Key packages

- `Microsoft.AspNetCore.Authentication.JwtBearer`
- `Microsoft.AspNetCore.Authentication.Negotiate`
- `Swashbuckle.AspNetCore`
- `Microsoft.AspNetCore.Components.WebAssembly`