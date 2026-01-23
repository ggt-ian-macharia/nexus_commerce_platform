namespace Identity.API.DTOs;

public record AuthResponse
{
    public string Token { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string? FirstName { get; init; }
    public string? LastName { get; init; }
    public DateTime ExpiresAt { get; init; }
}
