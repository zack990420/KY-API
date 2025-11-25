$baseUrl = "http://localhost:5086"

# 1. Register Admin
$registerBody = @{
    Username = "adminUser"
    Email = "admin@example.com"
    Password = "Password123!"
    Role = "Admin"
} | ConvertTo-Json

Write-Host "Registering Admin..."
try {
    $regResponse = Invoke-RestMethod -Uri "$baseUrl/api/Auth/register" -Method Post -Body $registerBody -ContentType "application/json"
    Write-Host "Registration Response: $($regResponse.Status)"
} catch {
    Write-Host "Registration failed (maybe already exists): $_"
}

# 2. Login
$loginBody = @{
    Username = "adminUser"
    Password = "Password123!"
} | ConvertTo-Json

Write-Host "Logging in..."
try {
    $loginResponse = Invoke-RestMethod -Uri "$baseUrl/api/Auth/login" -Method Post -Body $loginBody -ContentType "application/json"
    $token = $loginResponse.token
    Write-Host "Login Successful. Token received."
} catch {
    Write-Error "Login failed: $_"
    exit 1
}

# 3. Access Protected Endpoint (With Token)
Write-Host "Accessing Protected Endpoint (With Token)..."
try {
    $headers = @{ Authorization = "Bearer $token" }
    $data = Invoke-RestMethod -Uri "$baseUrl/WeatherForecast" -Headers $headers
    Write-Host "Success! Data count: $($data.Count)"
} catch {
    Write-Error "Access with token failed: $_"
}

# 4. Access Protected Endpoint (Without Token)
Write-Host "Accessing Protected Endpoint (Without Token)..."
try {
    Invoke-RestMethod -Uri "$baseUrl/WeatherForecast"
    Write-Error "Failed! Should have been unauthorized."
} catch {
    Write-Host "Success! Access denied as expected: $_"
}
