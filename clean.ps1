Write-Host 'WARNING: This will DROP the entire database!' -ForegroundColor Yellow
$confirmation = Read-Host 'Are you sure you want to continue? (yes/no)'

if ($confirmation -ne 'yes') {
    Write-Host 'Operation cancelled.' -ForegroundColor Gray
    exit
}

Write-Host 'Dropping database...' -ForegroundColor Cyan
dotnet ef database drop --force

if ($LASTEXITCODE -eq 0) {
    Write-Host 'Database dropped' -ForegroundColor Green
    
    Write-Host 'Recreating database with all tables...' -ForegroundColor Cyan
    dotnet ef database update
    
    if ($LASTEXITCODE -eq 0) {
        Write-Host 'Database recreated successfully!' -ForegroundColor Green
        Write-Host 'Note: Run the application to seed initial data' -ForegroundColor Yellow
    }
    if ($LASTEXITCODE -ne 0) {
        Write-Host 'Database recreation failed!' -ForegroundColor Red
    }
}
if ($LASTEXITCODE -ne 0) {
    Write-Host 'Database drop failed!' -ForegroundColor Red
}
