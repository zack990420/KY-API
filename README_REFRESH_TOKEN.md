# Refresh Token API

## Overview
The refresh token API allows users to obtain new access tokens without re-authenticating. This improves user experience while maintaining security.

## Endpoints

### 1. Login (Get Tokens)
```http
POST /api/Auth/login
Content-Type: application/json

{
  "username": "admin",
  "password": "Admin@123"
}
```

**Response:**
```json
{
  "accessToken": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "refreshToken": "base64-encoded-random-string",
  "expiration": "2025-11-25T15:25:00Z"
}
```

### 2. Refresh Token (Get New Tokens)
```http
POST /api/Auth/refresh-token
Content-Type: application/json

{
  "accessToken": "expired-or-valid-access-token",
  "refreshToken": "your-refresh-token"
}
```

**Response:**
```json
{
  "accessToken": "new-access-token",
  "refreshToken": "new-refresh-token",
  "expiration": "2025-11-25T18:25:00Z"
}
```

### 3. Revoke Token (Logout)
```http
POST /api/Auth/revoke-token
Authorization: Bearer {access-token}
```

**Response:**
```json
{
  "message": "Token revoked successfully"
}
```

## How It Works

### Token Lifecycle

1. **Login** → Receive access token (3 hours) + refresh token (7 days)
2. **Use access token** → Make API calls
3. **Access token expires** → Use refresh token to get new tokens
4. **Refresh token used** → Old refresh token invalidated, new one issued
5. **Logout** → Revoke refresh token

### Security Features

✅ **Token Rotation** - New refresh token generated on each refresh
✅ **Expiration** - Refresh tokens expire after 7 days (configurable)
✅ **One-time Use** - Old refresh token invalidated when used
✅ **Revocation** - Users can manually revoke tokens
✅ **Secure Generation** - Cryptographically secure random tokens

## Configuration

In `appsettings.json`:
```json
{
  "Jwt": {
    "RefreshTokenValidityInDays": 7
  }
}
```

## Usage Examples

### JavaScript/TypeScript

```typescript
// Store tokens
localStorage.setItem('accessToken', response.accessToken);
localStorage.setItem('refreshToken', response.refreshToken);

// API call with auto-refresh
async function apiCall(url, options) {
  let accessToken = localStorage.getItem('accessToken');
  
  let response = await fetch(url, {
    ...options,
    headers: {
      'Authorization': `Bearer ${accessToken}`,
      ...options.headers
    }
  });

  // If unauthorized, try to refresh
  if (response.status === 401) {
    const refreshToken = localStorage.getItem('refreshToken');
    const refreshResponse = await fetch('/api/Auth/refresh-token', {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({ accessToken, refreshToken })
    });

    if (refreshResponse.ok) {
      const tokens = await refreshResponse.json();
      localStorage.setItem('accessToken', tokens.accessToken);
      localStorage.setItem('refreshToken', tokens.refreshToken);
      
      // Retry original request
      response = await fetch(url, {
        ...options,
        headers: {
          'Authorization': `Bearer ${tokens.accessToken}`,
          ...options.headers
        }
      });
    } else {
      // Refresh failed, redirect to login
      window.location.href = '/login';
    }
  }

  return response;
}
```

### C# Client

```csharp
public class TokenService
{
    private string _accessToken;
    private string _refreshToken;

    public async Task<string> GetAccessTokenAsync()
    {
        // Check if token is expired (implement your logic)
        if (IsTokenExpired(_accessToken))
        {
            await RefreshTokensAsync();
        }
        return _accessToken;
    }

    private async Task RefreshTokensAsync()
    {
        var request = new { AccessToken = _accessToken, RefreshToken = _refreshToken };
        var response = await httpClient.PostAsJsonAsync("/api/Auth/refresh-token", request);
        
        if (response.IsSuccessStatusCode)
        {
            var tokens = await response.Content.ReadFromJsonAsync<TokenResponse>();
            _accessToken = tokens.AccessToken;
            _refreshToken = tokens.RefreshToken;
        }
    }
}
```

## Best Practices

### 1. Store Tokens Securely
- **Web**: Use httpOnly cookies (more secure than localStorage)
- **Mobile**: Use secure storage (Keychain/Keystore)
- **Never** store tokens in plain text files

### 2. Handle Token Expiration
```typescript
// Check token expiration before making requests
function isTokenExpired(token) {
  const payload = JSON.parse(atob(token.split('.')[1]));
  return payload.exp * 1000 < Date.now();
}
```

### 3. Implement Automatic Refresh
- Refresh tokens proactively before they expire
- Implement retry logic for failed requests

### 4. Logout Properly
```typescript
async function logout() {
  // Revoke token on server
  await fetch('/api/Auth/revoke-token', {
    method: 'POST',
    headers: { 'Authorization': `Bearer ${accessToken}` }
  });
  
  // Clear local storage
  localStorage.removeItem('accessToken');
  localStorage.removeItem('refreshToken');
}
```

## Token Lifetimes

| Token Type | Lifetime | Renewable |
|------------|----------|-----------|
| Access Token | 3 hours | Yes (via refresh token) |
| Refresh Token | 7 days | Yes (token rotation) |

## Security Considerations

⚠️ **Important Security Notes:**

1. **HTTPS Only** - Always use HTTPS in production
2. **Secure Storage** - Never expose refresh tokens in URLs or logs
3. **Token Rotation** - Refresh tokens are single-use
4. **Expiration** - Both tokens have expiration times
5. **Revocation** - Users can revoke tokens at any time

## Troubleshooting

### "Invalid refresh token"
- Refresh token has expired (> 7 days)
- Refresh token was already used
- Refresh token was revoked
- **Solution**: User must login again

### "Invalid access token"
- Access token format is invalid
- Access token was tampered with
- **Solution**: Check token format and generation

### Token not refreshing
- Ensure you're sending both accessToken and refreshToken
- Check that refresh token hasn't expired
- Verify configuration in appsettings.json
