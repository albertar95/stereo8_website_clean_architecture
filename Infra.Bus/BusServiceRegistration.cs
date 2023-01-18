using Application.Email.Contract;
using Application.MessageBroker.Bus;
using Infra.Bus.Bus;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Bus
{
    public static class BusServiceRegistration
    {
        public static IServiceCollection ConfigureBusService(this IServiceCollection services) 
        {
            services.AddScoped<IEventBus,RabbitMQBus>();
            return services;
        }
    }
}
