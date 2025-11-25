param(
    [string]$MigrationName = 'Baseline'
)

Write-Host 'WARNING: This will reset all migrations!' -ForegroundColor Yellow
$confirmation = Read-Host 'Are you sure you want to continue? (yes/no)'
if ($confirmation -ne 'yes') {
    Write-Host 'Operation cancelled.'
    exit
}

$date = Get-Date -Format 'yyyyMMdd'
$fullMigrationName = "${MigrationName}_${date}_001"

Write-Host 'Step 1: Dropping database...' -ForegroundColor Cyan
dotnet ef database drop --force
if ($LASTEXITCODE -ne 0) {
    Write-Host 'Database drop failed!' -ForegroundColor Red
    exit 1
}

Write-Host 'Step 2: Removing migration files...' -ForegroundColor Cyan
if (Test-Path 'Migrations') {
    Remove-Item -Path 'Migrations' -Recurse -Force
    Write-Host 'Migration files removed' -ForegroundColor Green
}

Write-Host 'Step 3: Creating baseline migration...' -ForegroundColor Cyan
dotnet ef migrations add $fullMigrationName
if ($LASTEXITCODE -ne 0) {
    Write-Host 'Migration creation failed!' -ForegroundColor Red
    exit 1
}

Write-Host 'Step 4: Applying migration...' -ForegroundColor Cyan
dotnet ef database update
if ($LASTEXITCODE -eq 0) {
    Write-Host 'Migration reset complete!' -ForegroundColor Green
}
