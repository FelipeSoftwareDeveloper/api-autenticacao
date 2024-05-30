using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace Blank.Infraestructure.IoC
{
    public static class AutoMapperConfiguration
    {

        public static IServiceCollection AddAutoMapperConfiguration(this IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            var mappingConfig = new MapperConfiguration(mc =>
            {
                //mc.AddProfile(new ProdutoMapperProfile());

            });

            IMapper mapper = mappingConfig.CreateMapper();

            services.AddSingleton(mapper);

            return services;
        }
    }
}
