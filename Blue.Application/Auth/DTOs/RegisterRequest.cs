namespace Blue.Application.Auth.DTOs;

public record RegisterRequest
(
    string Name,
    string Email,
    string Password
);