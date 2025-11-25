# Configuration Files

## Overview
Your project uses environment-specific configuration files that are merged at runtime.

## File Structure

```
appsettings.json                    ← Base (Production) settings
appsettings.Development.json        ← Development overrides
appsettings.Staging.json            ← Staging overrides (if needed)
```

## How It Works

### Development Environment
When `ASPNETCORE_ENVIRONMENT=Development`:
```
appsettings.json (Base)
    ↓ Override
appsettings.Development.json
    ↓ Result
Final Configuration (Merged)
```

### Production Environment
When `ASPNETCORE_ENVIRONMENT=Production`:
```
appsettings.json (Base)
    ↓ Result
Final Configuration (No overrides)
```

## Current Configuration

### appsettings.json (Production)
- **Database**: Production SQL Server (placeholder)
- **Azure Storage**: Production Azure account (placeholder)
- **Log Level**: Information
- **JWT/Hashids**: Shared across environments

### appsettings.Development.json (Development)
- **Database**: Local SQL Server (ZACK\base)
- **Azure Storage**: Azurite (local emulator)
- **Log Level**: Debug (more verbose)

## Environment Variables

Set in `Properties/launchSettings.json`:
```json
{
  "environmentVariables": {
    "ASPNETCORE_ENVIRONMENT": "Development"
  }
}
```

## What Gets Overridden

| Setting | appsettings.json | appsettings.Development.json | Used in Dev |
|---------|------------------|------------------------------|-------------|
| ConnectionString | Production DB | Local DB (ZACK) | ✅ Local DB |
| AzureStorage | Production | Azurite | ✅ Azurite |
| Log Level | Information | Debug | ✅ Debug |
| JWT Settings | Shared | - | ✅ Shared |
| Hashids | Shared | - | ✅ Shared |

## Best Practices

### ✅ DO:
- Keep production settings in `appsettings.json`
- Override only what's different in `appsettings.Development.json`
- Use environment variables for secrets in production
- Commit `appsettings.json` and `appsettings.Development.json` to git

### ❌ DON'T:
- Put production secrets in `appsettings.json` (use Azure Key Vault, environment variables)
- Duplicate all settings in development file
- Commit `appsettings.Production.json` with real secrets

## Deployment

### Development
```bash
dotnet run
# Uses: appsettings.json + appsettings.Development.json
```

### Production
```bash
export ASPNETCORE_ENVIRONMENT=Production
dotnet MyWebApi.dll
# Uses: appsettings.json only
```

### Docker
```dockerfile
ENV ASPNETCORE_ENVIRONMENT=Production
# Override with environment variables
ENV ConnectionStrings__DefaultConnection="Server=..."
```

## Checking Current Environment

Add this to any controller:
```csharp
private readonly IWebHostEnvironment _env;

public MyController(IWebHostEnvironment env)
{
    _env = env;
}

[HttpGet("environment")]
public IActionResult GetEnvironment()
{
    return Ok(new {
        Environment = _env.EnvironmentName,
        IsDevelopment = _env.IsDevelopment(),
        IsProduction = _env.IsProduction()
    });
}
```

## Security Notes

⚠️ **Important:**
- The production connection string in `appsettings.json` is a **placeholder**
- Replace it with your actual production database before deploying
- Consider using Azure Key Vault or environment variables for production secrets
- Never commit real production passwords to source control
