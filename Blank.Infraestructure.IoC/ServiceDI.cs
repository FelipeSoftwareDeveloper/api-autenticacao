using Blank.Application.Services;
using Blank.Domain.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Blank.Infraestructure.IoC
{
    public static class ServiceDI
    {
        public static IServiceCollection AddSeviceDI(this IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            services.AddScoped<IUserService, UserService>();

            return services;
        }
    }
}
