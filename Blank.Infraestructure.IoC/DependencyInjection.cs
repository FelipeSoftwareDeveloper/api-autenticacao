using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Blank.Infraestructure.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapperConfiguration();
            services.AddDbConfiguration(configuration);
            services.AddIdentityConfiguration();
            services.AddJWTConfiguration(configuration);
            services.AddRepositoryDI();
            services.AddSeviceDI();

            return services;
        }
    }
}
