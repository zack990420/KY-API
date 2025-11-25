# Logging with Serilog

## Overview
Your project uses **Serilog** for structured logging with automatic file rotation and separate error/info logs.

## Log Files

Logs are stored in the `Logs/` directory:

- **`Logs/info-YYYYMMDD.log`** - All logs (Information, Warning, Error, Fatal)
- **`Logs/error-YYYYMMDD.log`** - Only Error and Fatal logs

### Log Rotation
- **Daily rotation** - New log file created each day
- **Automatic naming** - Files named with date: `info-20251125.log`
- **Retention** - Old logs are kept (you can configure retention policy if needed)

## Log Levels

| Level | Description | Logged To |
|-------|-------------|-----------|
| **Information** | General info messages | info.log, Console |
| **Warning** | Warning messages | info.log, Console |
| **Error** | Error messages | info.log, error.log, Console |
| **Fatal** | Critical errors | info.log, error.log, Console |

## Using Logging in Your Code

### Inject ILogger

```csharp
public class MyController : ControllerBase
{
    private readonly ILogger<MyController> _logger;

    public MyController(ILogger<MyController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IActionResult Get()
    {
        _logger.LogInformation("Getting data");
        
        try
        {
            // Your code here
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to get data");
            return StatusCode(500);
        }
    }
}
```

### Log Examples

```csharp
// Information
_logger.LogInformation("User {UserId} logged in", userId);

// Warning
_logger.LogWarning("Slow query detected: {QueryTime}ms", queryTime);

// Error
_logger.LogError(ex, "Failed to process order {OrderId}", orderId);

// Fatal
_logger.LogCritical(ex, "Database connection failed");
```

## Log Format

Each log entry includes:
```
2025-11-25 12:19:49.123 +08:00 [INF] User 123 logged in
2025-11-25 12:19:50.456 +08:00 [ERR] Failed to process order 456
System.Exception: Order not found
   at MyWebApi.Controllers.OrderController.Process()
```

## Configuration

Logging is configured in `appsettings.json`:

```json
{
  "Serilog": {
    "MinimumLevel": {
      "Default": "Information",
      "Override": {
        "Microsoft": "Warning",
        "Microsoft.AspNetCore": "Warning"
      }
    }
  }
}
```

### Changing Log Levels

**Development** - Show more logs:
```json
"Default": "Debug"
```

**Production** - Show fewer logs:
```json
"Default": "Warning"
```

## Benefits

✅ **Separate Error Logs** - Easy to find errors in `error.log`
✅ **Structured Logging** - Logs include context and metadata
✅ **Daily Rotation** - Automatic log file management
✅ **Console + File** - Logs appear in both console and files
✅ **Performance** - Async logging doesn't block requests

## Viewing Logs

### During Development
- Logs appear in **Console** when running `dotnet run`
- Check `Logs/info-YYYYMMDD.log` for all logs
- Check `Logs/error-YYYYMMDD.log` for errors only

### In Production
- Monitor `Logs/error-YYYYMMDD.log` for issues
- Use log aggregation tools (e.g., Seq, ELK) for advanced analysis

## Best Practices

1. **Use structured logging** with parameters:
   ```csharp
   // ✅ Good
   _logger.LogInformation("User {UserId} created order {OrderId}", userId, orderId);
   
   // ❌ Bad
   _logger.LogInformation($"User {userId} created order {orderId}");
   ```

2. **Include context** in error logs:
   ```csharp
   _logger.LogError(ex, "Failed to send email to {Email}", userEmail);
   ```

3. **Use appropriate log levels**:
   - `Information` - Normal operations
   - `Warning` - Unexpected but handled
   - `Error` - Errors that need attention
   - `Fatal` - Critical failures

4. **Don't log sensitive data**:
   ```csharp
   // ❌ Never log passwords, tokens, etc.
   _logger.LogInformation("User logged in with password {Password}", password);
   ```
