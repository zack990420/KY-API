# 🚀 Using This Project as a Template

This repository is designed to be a **Production-Ready Starter Kit** for .NET 9 Web APIs. Follow this guide to start a new project from this codebase.

## 1. Clone & Rename

### Step 1: Clone the Repository
```bash
git clone <your-repo-url> NewProjectName
cd NewProjectName
```

### Step 2: Clean Up Git (Optional)
If you want a fresh git history for the new project:
```bash
rm -rf .git
git init
```

### Step 3: Rename Project Files (Manual or Script)
You need to rename `MyWebApi` to `NewProjectName` in:
1.  **Folder Names**: `MyWebApi` -> `NewProjectName`
2.  **Solution File**: `MyWebApi.sln` -> `NewProjectName.sln`
3.  **Project File**: `MyWebApi.csproj` -> `NewProjectName.csproj`
4.  **Namespaces**: Find & Replace `namespace MyWebApi` -> `namespace NewProjectName` in all `.cs` files.
5.  **LaunchSettings**: Update `Properties/launchSettings.json` if needed.

## 2. Setup Environment

### Step 1: Configure Database
Open `appsettings.Development.json` and update the database name:
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER;Database=NewProject_Dev;..."
}
```

### Step 2: Configure Secrets
Update `appsettings.json` with new secrets for the new project:
- **JWT Key**: Generate a new random string.
- **Hashids Salt**: Generate a new salt.

## 3. Reset Database

Since this is a new project, you want a fresh database schema without the old migration history.

### Run the Reset Script
```powershell
.\reset.ps1
```

**What this does:**
1.  Drops the database (if it exists from the old name).
2.  **Deletes all old migration files.**
3.  Creates a single new `Baseline` migration.
4.  Creates the new database.

## 4. Run & Verify

### Start the Application
```bash
dotnet run
```

### Verify Functionality
1.  **Swagger UI**: Go to `http://localhost:5086/swagger`
2.  **Log In**: Use the default seeded admin user (created by `DbSeeder`):
    - **Username**: `admin`
    - **Password**: `Admin@123`

## 5. Checklist for New Projects

- [ ] Renamed project and namespaces
- [ ] Updated `appsettings.json` (DB name, Secrets)
- [ ] Ran `.\reset.ps1` to create fresh DB
- [ ] Verified login works
- [ ] Committed initial state to new git repo

---

## 💡 Tips

- **Keep the Scripts**: The `reset.ps1`, `update.ps1`, etc., are generic. Keep them in your new project to manage migrations easily.
- **Custom Entities**: Modify `ApplicationUser.cs` early if you need different user properties.
- **Hangfire**: If you don't need background jobs, remove `Hangfire` packages and configuration from `Program.cs`.
