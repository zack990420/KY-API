using KYAPI.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace KYAPI.Data;

public class AppDbContext
    : IdentityDbContext<
        ApplicationUser,
        ApplicationRole,
        long,
        ApplicationUserClaim,
        ApplicationUserRole,
        ApplicationUserLogin,
        ApplicationRoleClaim,
        ApplicationUserToken
    >
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<WeatherForecastEntity> WeatherForecasts { get; set; }
    public DbSet<BlobFileEntity> BlobFiles { get; set; }
    public DbSet<EmailConfigEntity> EmailConfigs { get; set; }
    public DbSet<StaffInfo> StaffInfo { get; set; }
    public DbSet<StaffAddressProfile> StaffAddressProfile { get; set; }
    public DbSet<StaffEmergencyContact> StaffEmergencyContact { get; set; }
    public DbSet<StaffPayrollInfo> StaffPayrollInfo { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Configure bidirectional relationships FIRST (before setting delete behavior)
        // This tells EF Core how ApplicationUser navigation properties map to FK columns
        builder.Entity<ApplicationUser>()
            .HasOne(u => u.StaffInfo)
            .WithOne()
            .HasForeignKey<StaffInfo>(s => s.StaffUserId);

        builder.Entity<ApplicationUser>()
            .HasOne(u => u.StaffAddressProfile)
            .WithOne()
            .HasForeignKey<StaffAddressProfile>(s => s.StaffUserId);

        builder.Entity<ApplicationUser>()
            .HasOne(u => u.StaffPayrollInfo)
            .WithOne()
            .HasForeignKey<StaffPayrollInfo>(s => s.StaffUserId);

        builder.Entity<ApplicationUser>()
            .HasMany(u => u.StaffEmergencyContact)
            .WithOne(s => s.StaffUser)
            .HasForeignKey(s => s.StaffUserId);

        // THEN set ALL FKs to Restrict (including the ones configured above)
        // This prevents SQL Server circular cascade path errors
        foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
        {
            relationship.DeleteBehavior = DeleteBehavior.Restrict;
        }
    }
}
