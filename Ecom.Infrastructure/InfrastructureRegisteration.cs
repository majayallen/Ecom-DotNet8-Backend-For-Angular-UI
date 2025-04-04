using Ecom.Core.Interfaces;
using Ecom.Infrastructure.Data;
using Ecom.Infrastructure.Repositries;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ecom.Infrastructure
{
    public static class InfrastructureRegisteration
    {
        public static IServiceCollection infrastructureConfiguration(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddScoped(typeof(IGenericRepositry<>),typeof(GenericRepositry<>));
            //services.AddScoped<ICategoryRepositry,CategoryRepositry>();
            //services.AddScoped<IProductRepositry,ProductRepositry>();
            //services.AddScoped<IPhotoRepositry, PhotoRepositry>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            // apply DbContext

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("EcomDB"));
            });

            return services;
        }
    }
}
