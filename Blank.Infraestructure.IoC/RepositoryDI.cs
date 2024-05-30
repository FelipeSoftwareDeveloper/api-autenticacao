using Blank.Infraestructure.Data.Repository;
using Blank.Domain.Interfaces.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Blank.Infraestructure.IoC
{
    public static class RepositoryDI
    {
        public static IServiceCollection AddRepositoryDI(this IServiceCollection services)
        {

            if (services == null)
                throw new ArgumentNullException(nameof(services));

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            //services.AddScoped<IContaBancariaRepository, ContaBancariaRepository>();

            return services;
        }
    }
}
