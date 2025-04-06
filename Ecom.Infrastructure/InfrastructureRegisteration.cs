using Ecom.Core.Interfaces;
using Ecom.Core.Services;
using Ecom.Infrastructure.Data;
using Ecom.Infrastructure.Repositries;
using Ecom.Infrastructure.Repositries.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;

namespace Ecom.Infrastructure
{
    public static class InfrastructureRegisteration
    {
        public static IServiceCollection infrastructureConfiguration(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddScoped(typeof(IGenericRepositry<>),typeof(GenericRepositry<>));
           
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IImageManagementService, ImageManagementService>();
            services.AddSingleton<IFileProvider>(new
                PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")));
            // apply DbContext

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("EcomDB"));
            });

            return services;
        }
    }
}
