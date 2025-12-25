using CRM.DAL;
using CRM.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CRM.PL.Data
{
    public static class DbInitializer
    {
        public static async Task SeedAsync(
    IServiceProvider serviceProvider,
    IConfiguration configuration)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            // Ensure database is created
            await context.Database.MigrateAsync();

            // Seed roles
            await SeedRolesAsync(roleManager);

            // Seed admin user
            await SeedAdminUserAsync(userManager, configuration);

            // Seed technologies
            await SeedTechnologiesAsync(context);

            // Seed programmers
            await SeedProgrammersAsync(context);
        }

        private static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            string[] roles = { "Admin", "User" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }

        private static async Task SeedAdminUserAsync(
            UserManager<IdentityUser> userManager,
            IConfiguration configuration)
        {
            var adminEmail = configuration["AdminUser:Email"] ?? "admin@CRM.com";
            var adminPassword = configuration["AdminUser:Password"] ?? "Admin@123456";

            var adminUser = await userManager.FindByEmailAsync(adminEmail);

            if (adminUser == null)
            {
                adminUser = new IdentityUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(adminUser, adminPassword);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                    Console.WriteLine($"Admin user created: {adminEmail}");
                }
                else
                {
                    Console.WriteLine("Failed to create admin user:");
                    foreach (var error in result.Errors)
                    {
                        Console.WriteLine($"  - {error.Description}");
                    }
                }
            }
            else
            {
                Console.WriteLine("Admin user already exists");
            }
        }

        private static async Task SeedTechnologiesAsync(ApplicationDbContext context)
        {
            if (await context.Technologies.AnyAsync())
                return; // Already seeded

            var technologies = new List<Technology>
            {
                // Backend
                new Technology
                {
                    TechnologyName = "C#",
                    Category = "Backend",
                    DisplayOrder = 1,
                    IsActive = true,
                },
                new Technology
                {
                    TechnologyName = "ASP.NET Core",
                    Category = "Backend",
                    DisplayOrder = 2,
                    IsActive = true,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                },
                new Technology
                {
                    TechnologyName = "Entity Framework Core",
                    Category = "ORM",
                    DisplayOrder = 3,
                    IsActive = true,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                },
                // Frontend
                new Technology
                {
                    TechnologyName = "React",
                    Category = "Frontend",
                    DisplayOrder = 4,
                    IsActive = true,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                },
                new Technology
                {
                    TechnologyName = "Next.js",
                    Category = "Frontend",
                    DisplayOrder = 5,
                    IsActive = true,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                },
                new Technology
                {
                    TechnologyName = "TypeScript",
                    Category = "Frontend",
                    DisplayOrder = 6,
                    IsActive = true,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                },
                // Database
                new Technology
                {
                    TechnologyName = "SQL Server",
                    Category = "Database",
                    DisplayOrder = 7,
                    IsActive = true,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                },
                new Technology
                {
                    TechnologyName = "PostgreSQL",
                    Category = "Database",
                    DisplayOrder = 8,
                    IsActive = true,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                },
                // DevOps
                new Technology
                {
                    TechnologyName = "Docker",
                    Category = "DevOps",
                    DisplayOrder = 9,
                    IsActive = true,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                },
                new Technology
                {
                    TechnologyName = "Azure",
                    Category = "Cloud",
                    DisplayOrder = 10,
                    IsActive = true,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                }
            };

            await context.Technologies.AddRangeAsync(technologies);
            await context.SaveChangesAsync();
            Console.WriteLine($"Seeded {technologies.Count} technologies");
        }

        private static async Task SeedProgrammersAsync(ApplicationDbContext context)
        {
            if (await context.Programmers.AnyAsync())
                return; // Already seeded

            // Replace with your actual team data
            var programmers = new List<Programmer>
            {
                new Programmer
                {
                    Name = "Mohamed Ismail",
                    Title = "Software Engineer",
                    Brief = "Backend .NET and AI Machine Learning and Computer Vision",
                    Email = "MohamedIsmail@CRM.com",
                    PhoneNumber = "01234567890",
                    ImagePath = "/uploads/programmers/default.jpg",
                    YearsOfExperience = 2,
                    DisplayOrder = 1,
                    IsActive = true,
                },
                new Programmer
                {
                    Name = "Khaled Hany",
                    Title = "Software Engineer",
                    Brief = "Backend .NET",
                    Email = "KhaledHany@CRM.com",
                    PhoneNumber = "01234567891",
                    ImagePath = "/uploads/programmers/default.jpg",
                    YearsOfExperience = 3,
                    DisplayOrder = 2,
                    IsActive = true,
                },
                new Programmer
                {
                    Name = "Mohamed Khaled",
                    Title = "Full Stack Engineer",
                    Brief = "Backend.NET and Fronend Angular",
                    Email = "mohamedkhaled@CRM.com",
                    PhoneNumber = "01234567892",
                    ImagePath = "/uploads/programmers/default.jpg",
                    YearsOfExperience = 4,
                    DisplayOrder = 3,
                    IsActive = true,
                }
            };

            await context.Programmers.AddRangeAsync(programmers);
            await context.SaveChangesAsync();
            Console.WriteLine($"Seeded {programmers.Count} programmers");
        }
    }
}

