namespace MyWebApi.DTOs;

public record TokenResponse(string AccessToken, string RefreshToken, DateTime Expiration);
