param(
    [Parameter(Mandatory=$true)]
    [string]$MigrationName
)

$date = Get-Date -Format 'yyyyMMdd'
$migrationsFolder = 'Migrations'
$runningNumber = 1

if (Test-Path $migrationsFolder) {
    $pattern = "${MigrationName}_${date}_*"
    $existingMigrations = Get-ChildItem -Path $migrationsFolder -Filter '*.cs' | Where-Object { $_.Name -like $pattern }
    $runningNumber = $existingMigrations.Count + 1
}

$fullMigrationName = "${MigrationName}_${date}_$('{0:D3}' -f $runningNumber)"

Write-Host "Creating migration: $fullMigrationName" -ForegroundColor Cyan
dotnet ef migrations add $fullMigrationName

if ($LASTEXITCODE -eq 0) {
    Write-Host 'Migration created successfully!' -ForegroundColor Green
    
    Write-Host 'Applying migration to database...' -ForegroundColor Cyan
    dotnet ef database update
    
    if ($LASTEXITCODE -eq 0) {
        Write-Host 'Database updated successfully!' -ForegroundColor Green
    }
    if ($LASTEXITCODE -ne 0) {
        Write-Host 'Database update failed!' -ForegroundColor Red
    }
}

if ($LASTEXITCODE -ne 0) {
    Write-Host 'Migration creation failed!' -ForegroundColor Red
}
