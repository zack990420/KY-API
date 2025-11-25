Write-Host 'Migration Rollback Script' -ForegroundColor Cyan
Write-Host '=========================' -ForegroundColor Cyan
Write-Host ''

Write-Host 'Fetching migration history...' -ForegroundColor Yellow
$migrations = dotnet ef migrations list --no-build 2>&1

if ($LASTEXITCODE -ne 0) {
    Write-Host 'Error: Failed to get migration list. Make sure the project builds successfully.' -ForegroundColor Red
    exit 1
}

$migrationLines = $migrations | Where-Object { $_ -match '^\d{14}_' }

if ($migrationLines.Count -eq 0) {
    Write-Host 'No migrations found to rollback.' -ForegroundColor Yellow
    exit 0
}

$lastMigration = ($migrationLines[0] -split ' ')[0]
Write-Host "Last migration: $lastMigration" -ForegroundColor White

if ($migrationLines.Count -gt 1) {
    $previousMigration = ($migrationLines[1] -split ' ')[0]
    Write-Host "Will rollback to: $previousMigration" -ForegroundColor White
}
if ($migrationLines.Count -le 1) {
    Write-Host 'Will rollback to: (empty database)' -ForegroundColor White
    $previousMigration = '0'
}

Write-Host ''
Write-Host 'This will:' -ForegroundColor Yellow
Write-Host '1. Revert database to previous migration' -ForegroundColor Yellow
Write-Host '2. Remove the latest migration files' -ForegroundColor Yellow
Write-Host ''

$confirmation = Read-Host 'Continue? (y/n)'
if ($confirmation -ne 'y') {
    Write-Host 'Operation cancelled.' -ForegroundColor Green
    exit 0
}

Write-Host ''
Write-Host 'Step 1: Reverting database to previous migration...' -ForegroundColor Cyan
dotnet ef database update $previousMigration

if ($LASTEXITCODE -ne 0) {
    Write-Host 'Error: Failed to revert database.' -ForegroundColor Red
    exit 1
}

Write-Host ''
Write-Host 'Step 2: Removing migration files...' -ForegroundColor Cyan
dotnet ef migrations remove --force

if ($LASTEXITCODE -ne 0) {
    Write-Host 'Error: Failed to remove migration files.' -ForegroundColor Red
    exit 1
}

Write-Host ''
Write-Host 'Rollback completed successfully!' -ForegroundColor Green
Write-Host "Migration '$lastMigration' has been removed." -ForegroundColor Green
