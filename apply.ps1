Write-Host 'Applying migrations to database...' -ForegroundColor Cyan
dotnet ef database update

if ($LASTEXITCODE -eq 0) {
    Write-Host 'Database updated successfully!' -ForegroundColor Green
}
if ($LASTEXITCODE -ne 0) {
    Write-Host 'Database update failed!' -ForegroundColor Red
}
