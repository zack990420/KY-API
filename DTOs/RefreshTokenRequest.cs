namespace MyWebApi.DTOs;

public record RefreshTokenRequest(string AccessToken, string RefreshToken);
