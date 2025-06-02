using Application.Interfaces;
using Application.Mappers;
using Application.UseCases.ClientUseCases;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            #region AutoMapper
            MapperConfiguration mapperConfig = new(cfg =>
            {
                cfg.AddProfile<ClientMappers>();
            });

            IMapper mapper = mapperConfig.CreateMapper();

            services.AddSingleton(mapper);
            #endregion

            #region Use Cases
            services.AddScoped<IRegisterClientUseCase, RegisterClientUseCase>();
            services.AddScoped<IFilterClientsUseCase, FilterClientsUseCase>();
            services.AddScoped<IRetrieveClientUseCase, RetrieveClientUseCase>();
            services.AddScoped<IClientIdentificationUseCase, ClientIdentificationUseCase>();
            #endregion

            return services;
        }
    }
}
