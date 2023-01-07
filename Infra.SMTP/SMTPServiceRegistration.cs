using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Email.Contract;

namespace Infra.SMTP
{
    public static class SMTPServiceRegistration
    {
        public static IServiceCollection ConfigureSMTPService(this IServiceCollection services)
        {
            services.AddScoped<IMailActions, MailActions>();
            return services;
        }
    }
}
