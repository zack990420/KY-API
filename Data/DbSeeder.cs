using KYAPI.Entities;
using Microsoft.AspNetCore.Identity;

namespace KYAPI.Data;

public static class DbSeeder
{
    public static async Task SeedRolesAsync(RoleManager<ApplicationRole> roleManager)
    {
        string[] roleNames = { "Admin", "User" };

        foreach (var roleName in roleNames)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                await roleManager.CreateAsync(new ApplicationRole { Name = roleName });
            }
        }
    }

    public static async Task SeedUsersAsync(UserManager<ApplicationUser> userManager)
    {
        // Seed Admin User
        if (await userManager.FindByNameAsync("admin") == null)
        {
            var adminUser = new ApplicationUser
            {
                UserName = "admin",
                Email = "admin@mywebapi.com",
                EmailConfirmed = true,
            };

            var result = await userManager.CreateAsync(adminUser, "Admin@123");
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }
        }

        // Seed Regular User
        if (await userManager.FindByNameAsync("testuser") == null)
        {
            var testUser = new ApplicationUser
            {
                UserName = "testuser",
                Email = "testuser@mywebapi.com",
                EmailConfirmed = true,
            };

            var result = await userManager.CreateAsync(testUser, "User@123");
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(testUser, "User");
            }
        }
    }

    public static async Task SeedWeatherDataAsync(AppDbContext context)
    {
        if (!context.WeatherForecasts.Any())
        {
            var summaries = new[]
            {
                "Freezing",
                "Bracing",
                "Chilly",
                "Cool",
                "Mild",
                "Warm",
                "Balmy",
                "Hot",
                "Sweltering",
                "Scorching",
            };

            var weatherData = new List<WeatherForecastEntity>();
            var random = new Random();

            for (int i = 1; i <= 10; i++)
            {
                weatherData.Add(
                    new WeatherForecastEntity
                    {
                        Date = DateOnly.FromDateTime(DateTime.Now.AddDays(i)),
                        TemperatureC = random.Next(-20, 55),
                        Summary = summaries[random.Next(summaries.Length)],
                    }
                );
            }

            context.WeatherForecasts.AddRange(weatherData);
            await context.SaveChangesAsync();
        }
    }

    public static async Task SeedEmailConfigAsync(AppDbContext context)
    {
        if (!context.EmailConfigs.Any())
        {
            var emailConfig = new EmailConfigEntity
            {
                SmtpHost = "smtp.gmail.com",
                SmtpPort = 587,
                SmtpUsername = "your-email@gmail.com",
                SmtpPassword = "your-app-password",
                FromEmail = "noreply@mywebapi.com",
                FromName = "MyWebApi",
                EnableSsl = true,
                IsActive = true,
            };

            context.EmailConfigs.Add(emailConfig);
            await context.SaveChangesAsync();
        }
    }
}
