# Database Migration Scripts

Simple scripts for daily database management.

## Commands

### 1. Update (Create & Apply Migration)
Run this after making changes to your entity models:
```powershell
./update.ps1 "AddUserAge"
```
This will create a migration with format: `AddUserAge_20251124_001`
- Name: Your description
- Date: yyyyMMdd
- Running number: Auto-incremented (001, 002, 003...)

The script will:
- Create a new migration with the formatted name
- Apply it to the database automatically

### 2. Apply (Update Database Only)
Run this to apply existing pending migrations:
```powershell
./apply.ps1
```

### 3. Clean (Drop & Recreate)
Run this to drop the database and recreate it from scratch:
```powershell
./clean.ps1
```
⚠️ **Warning**: This deletes all data!

### 4. Rollback (Undo Last Migration)
Run this to undo the most recent migration:
```powershell
./rollback.ps1
```
This will:
1. Revert the database to the previous migration state
2. Remove the latest migration files

⚠️ **Warning**: This undoes database changes from the last migration!

### 5. Reset (Consolidate Migrations)
Run this when you have too many migration files and want to start fresh:
```powershell
./reset.ps1
# Or with custom baseline name:
./reset.ps1 "MyBaseline"
```
This will:
1. Drop the database
2. Remove all migration files
3. Create a single fresh baseline migration
4. Apply it to the database

⚠️ **Warning**: This deletes all data and migration history!

## Typical Workflow

1. Make changes to entity classes (e.g., add a new property to `WeatherForecastEntity.cs`)
2. Run `./update.ps1 "DescriptionOfChange"` - Done! Migration created and applied
3. Run the app - data will be seeded automatically

## Migration Naming Examples

```powershell
./update.ps1 "AddUserAge"          # → AddUserAge_20251124_001
./update.ps1 "UpdateWeatherTable"  # → UpdateWeatherTable_20251124_001
./update.ps1 "AddUserAge"          # → AddUserAge_20251124_002 (if run again same day)
```

## When to Use Each Command

- **`update.ps1`** - After changing models (most common)
- **`apply.ps1`** - When pulling migrations from git
- **`clean.ps1`** - When you need a fresh database (keeps migrations)
- **`rollback.ps1`** - When you need to undo the last migration
- **`reset.ps1`** - When you have too many migrations and want to consolidate

## Troubleshooting

If scripts don't run, enable execution:
```powershell
Set-ExecutionPolicy -ExecutionPolicy RemoteSigned -Scope CurrentUser
```
