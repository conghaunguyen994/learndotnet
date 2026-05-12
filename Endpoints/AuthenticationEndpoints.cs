using learndotnet.DTOs.Auth;
using learndotnet.Services;

namespace learndotnet.Endpoints;

public static class AuthenticationEndpoints
{
    public static void MapAuthenticationEndpoints(this WebApplication app)
    {
        // POST /auth/login
        app.MapPost("/auth/login", (LoginRequest request, AuthenticationService authService) =>
        {
            var result = authService.Login(request);
            if (result == null)
            {
                return Results.Unauthorized();
            }

            return Results.Ok(result);
        }).WithName("Login")
        .AllowAnonymous();

        // POST /auth/refresh
        app.MapPost("/auth/refresh", (RefreshTokenRequest request, AuthenticationService authService) =>
        {
            var result = authService.RefreshToken(request.RefreshToken);
            if (result == null)
            {
                return Results.Unauthorized();
            }

            return Results.Ok(result);
        }).WithName("RefreshToken")
        .AllowAnonymous();

        // POST /auth/register
        app.MapPost("/auth/register", (RegisterRequest request, AuthenticationService authService) =>
        {
            var (success, message, token) = authService.Register(request);
            if (!success)
            {
                return Results.BadRequest(new { message });
            }

            return Results.Created("/auth/register", new { message, token });
        }).WithName("Register")
        .AllowAnonymous();
    }
}