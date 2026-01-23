using System.ComponentModel.DataAnnotations;

namespace Identity.DTOs;

public record RegisterRequest
{
    [Required]
    [EmailAddress]
    public string Email { get; init; } = string.Empty;

    [Required]
    [MinLength(6)]
    public string Password { get; init; } = string.Empty;

    public string? FirstName { get; init; }
    public string? LastName { get; init; }
}
