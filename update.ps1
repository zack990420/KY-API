param(
    [Parameter(Mandatory=$true)]
    [string]$MigrationName
)

$date = Get-Date -Format 'yyyyMMdd'
$migrationsFolder = 'Migrations'
$runningNumber = 1

if (Test-Path $migrationsFolder) {
    # Look for existing migrations with same name and date (exclude .Designer.cs files)
    $pattern = "${date}*_${MigrationName}_${date}_*.cs"
    $existingMigrations = Get-ChildItem -Path $migrationsFolder -Filter '*.cs' | 
        Where-Object { $_.Name -match "${date}\d+_${MigrationName}_${date}_\d+\.cs$" }
    
    if ($existingMigrations) {
        # Extract the running numbers and get the max
        $numbers = $existingMigrations | ForEach-Object {
            if ($_.Name -match '_(\d{3})\.cs$') {
                [int]$matches[1]
            }
        }
        $runningNumber = ($numbers | Measure-Object -Maximum).Maximum + 1
    }
}

$fullMigrationName = "{0}_{1}_{2:D3}" -f $MigrationName, $date, $runningNumber

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
