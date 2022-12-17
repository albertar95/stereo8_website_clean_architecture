using Application.Persistance.Contracts;
using Infra.Persistance.Context;
using Infra.Persistance.Repository;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Persistance
{
    public static class PersistanceServiceRegisteration
    {
        public static IServiceCollection ConfigurePersistanceService(this IServiceCollection services) 
        {
            services.AddDbContext<AudioShopDbContext>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            return services;
        }
    }
}
