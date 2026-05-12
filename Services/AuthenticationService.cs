using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using learndotnet.DTOs.Auth;
using learndotnet.Models;
using learndotnet.Repositories;
using Microsoft.IdentityModel.Tokens;

namespace learndotnet.Services;

public class AuthenticationService
{
    private readonly IUserRepository _userRepository;
    private readonly IConfiguration _configuration;
    private readonly ILogger<AuthenticationService> _logger;

    public AuthenticationService(IUserRepository userRepository, IConfiguration configuration, ILogger<AuthenticationService> logger)
    {
        _userRepository = userRepository;
        _configuration = configuration;
        _logger = logger;
    }

    public TokenResponse? Login(LoginRequest request)
    {
        var user = _userRepository.GetAllUsers().FirstOrDefault(u => u.Email == request.Email);
        if (user == null)
        {
            _logger.LogWarning("Login attempt failed for email: {Email}", request.Email);
            return null;
        }

        // In production, use proper password hashing (bcrypt, etc.)
        // This is a simple example - do NOT use in production
        if (user.Email != request.Email)
        {
            _logger.LogWarning("Invalid credentials for email: {Email}", request.Email);
            return null;
        }

        var token = GenerateAccessToken(user);
        var refreshToken = GenerateRefreshToken();

        return new TokenResponse
        {
            AccessToken = token,
            RefreshToken = refreshToken,
            ExpiresAt = DateTime.UtcNow.AddMinutes(int.Parse(_configuration["JwtSettings:ExpirationMinutes"] ?? "15"))
        };
    }

    public string GenerateAccessToken(User user)
    {
        var jwtSettings = _configuration.GetSection("JwtSettings");
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Secret"] ?? ""));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Name, user.Name)
        };

        var token = new JwtSecurityToken(
            issuer: "LearnDotnet",
            audience: "LearnDotnetUsers",
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(int.Parse(jwtSettings["ExpirationMinutes"] ?? "15")),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string GenerateRefreshToken()
    {
        var randomNumber = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    public TokenResponse? RefreshToken(string refreshToken)
    {
        // In production, store refresh tokens in database with expiration
        // This is a simple example without token storage
        try
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Secret"] ?? ""));

            var tokenHandler = new JwtSecurityTokenHandler();
            tokenHandler.ValidateToken(refreshToken, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = key,
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            var newAccessToken = GenerateAccessToken(new User { Id = 1, Email = "", Name = "" });
            var newRefreshToken = GenerateRefreshToken();

            return new TokenResponse
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken,
                ExpiresAt = DateTime.UtcNow.AddMinutes(int.Parse(jwtSettings["ExpirationMinutes"] ?? "15"))
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error refreshing token");
            return null;
        }
    }

    public (bool Success, string Message, TokenResponse? Token) Register(RegisterRequest request)
    {
        // Validate input
        if (string.IsNullOrWhiteSpace(request.Name))
        {
            return (false, "Name is required", null);
        }

        if (string.IsNullOrWhiteSpace(request.Email))
        {
            return (false, "Email is required", null);
        }

        if (string.IsNullOrWhiteSpace(request.Password))
        {
            return (false, "Password is required", null);
        }

        if (request.Password.Length < 6)
        {
            return (false, "Password must be at least 6 characters", null);
        }

        if (request.Password != request.ConfirmPassword)
        {
            return (false, "Passwords do not match", null);
        }

        // Check if email already exists
        var existingUser = _userRepository.GetAllUsers().FirstOrDefault(u => u.Email == request.Email);
        if (existingUser != null)
        {
            _logger.LogWarning("Registration failed: Email already exists - {Email}", request.Email);
            return (false, "Email already exists", null);
        }

        // Create new user
        var newUser = new User
        {
            Name = request.Name,
            Email = request.Email
            // Note: In production, hash the password using bcrypt or similar
        };

        try
        {
            _userRepository.AddUser(newUser);

            var token = GenerateAccessToken(newUser);
            var refreshToken = GenerateRefreshToken();

            var response = new TokenResponse
            {
                AccessToken = token,
                RefreshToken = refreshToken,
                ExpiresAt = DateTime.UtcNow.AddMinutes(int.Parse(_configuration["JwtSettings:ExpirationMinutes"] ?? "15"))
            };

            _logger.LogInformation("User registered successfully: {Email}", request.Email);
            return (true, "Registration successful", response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during registration for email: {Email}", request.Email);
            return (false, "An error occurred during registration", null);
        }
    }
}