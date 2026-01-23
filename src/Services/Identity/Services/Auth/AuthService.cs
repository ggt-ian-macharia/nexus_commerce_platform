using Identity.DTOs;
using Identity.Models;
using Identity.Repositories.User;
using Identity.Services.Token;

namespace Identity.Services.Auth;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly TokenService _tokenService;

    public AuthService(IUserRepository userRepository, TokenService tokenService)
    {
        _userRepository = userRepository;
        _tokenService = tokenService;
    }

    public async Task<AuthResponse?> RegisterAsync(RegisterRequest request)
    {
        var user = new ApplicationUser
        {
            UserName = request.Email,
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName
        };

        var success = await _userRepository.CreateAsync(user, request.Password);

        if (!success)
        {
            return null;
        }

        var token = _tokenService.GenerateToken(user);

        return new AuthResponse
        {
            Token = token,
            Email = user.Email!,
            FirstName = user.FirstName,
            LastName = user.LastName,
            ExpiresAt = DateTime.UtcNow.AddHours(24)
        };
    }

    public async Task<AuthResponse?> LoginAsync(LoginRequest request)
    {
        var user = await _userRepository.FindByEmailAsync(request.Email);

        if (user == null || !await _userRepository.CheckPasswordAsync(user, request.Password))
        {
            return null;
        }

        var token = _tokenService.GenerateToken(user);

        return new AuthResponse
        {
            Token = token,
            Email = user.Email!,
            FirstName = user.FirstName,
            LastName = user.LastName,
            ExpiresAt = DateTime.UtcNow.AddHours(24)
        };
    }
}
