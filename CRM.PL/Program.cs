using AutoMapper;
using CRM.BLL;
using CRM.BLL.Interfaces;
using CRM.BLL.Interfaces.Services;
using CRM.BLL.Mapping;
using CRM.BLL.Services;
using CRM.DAL;
using CRM.PL.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace CRM.PL
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add DbContext
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("DefaultConnection")
                ));


            // Add UnitOfWork and Services
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<ITestimonialService, TestimonialService>();
            builder.Services.AddScoped<IProjectService, ProjectService>();
            builder.Services.AddScoped<IInquiryService, InquiryService>();
            builder.Services.AddScoped<IProgrammerService, ProgrammerService>();
            builder.Services.AddScoped<IDashboardService, DashboardService>();
            builder.Services.AddScoped<IFileService, FileService>();
            builder.Services.AddScoped<ITokenService, TokenService>();

            builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                // Password settings
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 8;

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings
                options.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            builder.Services.AddAutoMapper(cfg => cfg.AddProfile<MappingProfile>(), typeof(Program));

            var jwtSettings = builder.Configuration.GetSection("JWT");
            var jwtKey = jwtSettings["Key"] ?? throw new InvalidOperationException("JWT Key not found");
            var jwtIssuer = jwtSettings["Issuer"] ?? throw new InvalidOperationException("JWT Issuer not found");
            var jwtAudience = jwtSettings["Audience"] ?? throw new InvalidOperationException("JWT Audience not found");

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtIssuer,
                    ValidAudience = jwtAudience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
                    ClockSkew = TimeSpan.Zero
                };
            });
            // Add services to the container.
            builder.Services.AddMemoryCache();
            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                // Handle circular references
                options.JsonSerializerOptions.ReferenceHandler =
                    System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;

                // Format dates consistently
                options.JsonSerializerOptions.Converters.Add(
                    new System.Text.Json.Serialization.JsonStringEnumConverter());
            });

            builder.Services.AddEndpointsApiExplorer();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();
            if (app.Environment.IsDevelopment())
            {
                using var scope = app.Services.CreateScope();
                try
                {
                    var mapper = scope.ServiceProvider.GetRequiredService<IMapper>();
                    mapper.ConfigurationProvider.AssertConfigurationIsValid();
                    Console.WriteLine("AutoMapper configuration is valid!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"AutoMapper configuration error: {ex.Message}");
                }
            }

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    await DbInitializer.SeedAsync(services, app.Configuration);
                    Console.WriteLine("Database seeding completed successfully");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred while seeding the database: {ex.Message}");
                }
            }
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
