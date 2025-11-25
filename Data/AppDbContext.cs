using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using KYAPI.Entities;

namespace KYAPI.Data;

public class AppDbContext : IdentityDbContext<
    ApplicationUser,
    ApplicationRole,
    long,
    ApplicationUserClaim,
    ApplicationUserRole,
    ApplicationUserLogin,
    ApplicationRoleClaim,
    ApplicationUserToken>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<WeatherForecastEntity> WeatherForecasts { get; set; }
    public DbSet<BlobFileEntity> BlobFiles { get; set; }
    public DbSet<EmailConfigEntity> EmailConfigs { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}
