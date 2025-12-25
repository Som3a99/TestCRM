using CRM.BLL.Interfaces;
using CRM.BLL.Interfaces.Repositories;
using CRM.BLL.Interfaces.Services;
using CRM.BLL.Mapping;
using CRM.BLL.Repositories;
using CRM.BLL.Services;
using CRM.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;

namespace CRM.BLL.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBusinessLayer(
            this IServiceCollection services,
            string connectionString)
        {
            // Register DbContext
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

            // Register AutoMapper
            services.AddAutoMapper(cfg => cfg.AddProfile<MappingProfile>());

            // Register Unit of Work
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Register Repositories
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IInquiryRepository, InquiryRepository>();
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<IProgrammerRepository, ProgrammerRepository>();
            services.AddScoped<ITechnologyRepository, TechnologyRepository>();
            services.AddScoped<ITestimonialRepository, TestimonialRepository>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            // Register Services
            services.AddScoped<IInquiryService, InquiryService>();
            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<ITestimonialService, TestimonialService>();
            services.AddScoped<IProgrammerService, ProgrammerService>();
            services.AddScoped<IDashboardService, DashboardService>();
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<ITokenService, TokenService>();

            // Register Memory Cache
            services.AddMemoryCache();

            return services;
        }
    }
}
