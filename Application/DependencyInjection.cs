using Application.Interfaces;
using Application.Mappers;
using Application.UseCases.ClientUseCases;
using Application.UseCases.ProductCategoryUseCases;
using Application.UseCases.UserUseCases;
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
                cfg.AddProfile<UserMappers>();
                cfg.AddProfile<ProductCategoryMappers>();
            });

            IMapper mapper = mapperConfig.CreateMapper();

            services.AddSingleton(mapper);
            #endregion

            #region Use Cases
            services.AddScoped<IRegisterClientUseCase, RegisterClientUseCase>();
            services.AddScoped<IFilterClientsUseCase, FilterClientsUseCase>();
            services.AddScoped<IRetrieveClientUseCase, RetrieveClientUseCase>();
            services.AddScoped<IClientIdentificationUseCase, ClientIdentificationUseCase>();

            services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
            services.AddScoped<IFilterUserUseCase, FilterUserUseCase>();
            services.AddScoped<IGetUserUseCase, GetUserUseCase>();
            services.AddScoped<IAuthenticateUserUseCase, AuthenticateUserUseCase>();

            services.AddScoped<IRegisterProductCategoryUseCase, RegisterProductCategoryUseCase>();
            services.AddScoped<IFilterProductCategoryUseCase, FilterProductCategoryUseCase>();
            services.AddScoped<IGetProductCategoryUseCase, GetProductCategoryUseCase>();
            #endregion

            return services;
        }
    }
}
