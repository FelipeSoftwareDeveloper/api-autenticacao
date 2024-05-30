using Blank.Infraestructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Blank.Infraestructure.IoC
{
    public static class BancoDados
    {
        public static IServiceCollection AddDbConfiguration(this IServiceCollection services, IConfiguration configuration)
        {

            string connectionString = configuration.GetConnectionString("DefaultConnection");
            services
                .AddDbContext<AppDbContext>(options =>
                        options
                            .UseNpgsql(connectionString)
                            .EnableSensitiveDataLogging()
                            .LogTo(Console.WriteLine, LogLevel.Information)
                    );

            return services;
        }
    }

}
