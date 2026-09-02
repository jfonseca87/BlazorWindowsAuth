using Microsoft.AspNetCore.Authentication.Negotiate;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("test", policy =>
    {
        policy.WithOrigins("http://localhost:5244")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

builder.Services.AddAuthentication(options =>
{
    // Set the default scheme to our policy scheme.
    options.DefaultScheme = "DynamicScheme";
    options.DefaultChallengeScheme = "DynamicScheme";
})
.AddNegotiate("Negotiate", options =>
{
    // Configure Windows authentication (Negotiate)
})
.AddJwtBearer("Bearer", options =>
{
    // Configure JWT token validation here.
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        // Other JWT validation parameters...
    };
})
.AddPolicyScheme("DynamicScheme", "Dynamic Authentication Scheme", options =>
{
    options.ForwardDefaultSelector = context =>
    {
        // Check for an Authorization header starting with "Bearer "
        var authHeader = context.Request.Headers["Authorization"].FirstOrDefault();
        if (!string.IsNullOrEmpty(authHeader) && authHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
        {
            return "Bearer";
        }
        // Fallback: use Windows (Negotiate) authentication
        return "Negotiate";
    };
});


builder.Services.AddAuthorization();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("test");

app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/api/oauth", (HttpContext httpContext) =>
{
    // Return the authenticated user's Windows username
    return Results.Ok(httpContext.User.Identity.Name);
})
.RequireAuthorization()
.WithName("GetOAuth");

app.MapGet("/api/test", (HttpContext httpContext) =>
{
    // Return the authenticated user's Windows username
    return Results.Ok("test using jwt");
})
.RequireAuthorization()
.WithName("GetTest");

app.Run();
