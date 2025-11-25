# Custom ASP.NET Identity Entities

## Overview
Your project now uses custom entity classes for ASP.NET Identity instead of the default `IdentityUser<long>` and `IdentityRole<long>`. This gives you full control over the identity tables and allows you to add custom properties.

## Entity Files Created

All custom identity entities are located in `Entities/`:

1. **ApplicationUser.cs** - Custom user entity (extends `IdentityUser<long>`)
2. **ApplicationRole.cs** - Custom role entity (extends `IdentityRole<long>`)
3. **ApplicationUserClaim.cs** - User claims (extends `IdentityUserClaim<long>`)
4. **ApplicationUserRole.cs** - User-role relationships (extends `IdentityUserRole<long>`)
5. **ApplicationUserLogin.cs** - External login providers (extends `IdentityUserLogin<long>`)
6. **ApplicationRoleClaim.cs** - Role claims (extends `IdentityRoleClaim<long>`)
7. **ApplicationUserToken.cs** - User tokens (extends `IdentityUserToken<long>`)

## Database Tables Mapping

| Entity Class | Database Table |
|--------------|----------------|
| ApplicationUser | AspNetUsers |
| ApplicationRole | AspNetRoles |
| ApplicationUserClaim | AspNetUserClaims |
| ApplicationUserRole | AspNetUserRoles |
| ApplicationUserLogin | AspNetUserLogins |
| ApplicationRoleClaim | AspNetRoleClaims |
| ApplicationUserToken | AspNetUserTokens |

## Adding Custom Properties

### Example: Add properties to ApplicationUser

```csharp
public class ApplicationUser : IdentityUser<long>
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string? ProfilePictureUrl { get; set; }
}
```

### Example: Add properties to ApplicationRole

```csharp
public class ApplicationRole : IdentityRole<long>
{
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public bool IsSystemRole { get; set; }
}
```

## After Adding Custom Properties

1. **Create a migration:**
   ```bash
   ./update.ps1 "AddUserCustomProperties"
   ```

2. **The migration will automatically add the new columns to the database**

## Files Updated

The following files were updated to use the custom entities:

- `Data/AppDbContext.cs` - Uses custom identity entities
- `Program.cs` - Registers `ApplicationUser` and `ApplicationRole`
- `Controllers/AuthController.cs` - Uses `ApplicationUser` and `ApplicationRole`
- `Data/DbSeeder.cs` - Seeds `ApplicationUser` and `ApplicationRole`
- `Entities/BlobFileEntity.cs` - References `ApplicationUser`

## Benefits

✅ **Full Control** - You own the entity classes and can modify them
✅ **Custom Properties** - Add any properties you need to users/roles
✅ **Type Safety** - Strongly typed entities instead of generic types
✅ **IntelliSense** - Better IDE support for your custom properties
✅ **Extensibility** - Easy to extend with navigation properties and relationships

## Important Notes

- The entities still use `long` as the primary key type
- All ASP.NET Identity features continue to work normally
- Existing migrations are not affected (they reference the old types)
- Future migrations will use the new custom entity types
