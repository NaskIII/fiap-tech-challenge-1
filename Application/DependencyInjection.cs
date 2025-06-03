using Application.Interfaces;
using Application.Mappers;
using Application.UseCases.CheckoutUseCases;
using Application.UseCases.ClientUseCases;
using Application.UseCases.KitchenQueueUseCases;
using Application.UseCases.OrderUseCase;
using Application.UseCases.ProductCategoryUseCases;
using Application.UseCases.ProductUseCases;
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
                cfg.AddProfile<ProductMappers>();
                cfg.AddProfile<OrderMappers>();
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

            services.AddScoped<IFilterProductUseCase, FilterProductUseCase>();
            services.AddScoped<IGetProductUseCase, GetProductUseCase>();
            services.AddScoped<IUpdateProductUseCase, UpdateProductUseCase>();
            services.AddScoped<IRegisterProductUseCase, RegisterProductUseCase>();

            services.AddScoped<ICreateOrderUseCase, CreateOrderUseCase>();
            services.AddScoped<IFilterOrderUseCase, FilterOrderUseCase>();
            services.AddScoped<IGetOrderUseCase, GetOrderUseCase>();
            
            services.AddScoped<ICheckoutUseCase, CheckoutUseCase>();

            services.AddScoped<IKitchenQueueListQueue, KitchenQueueListQueueUseCase>();
            services.AddScoped<IKitchenEnqueueOrderUseCase, KitchenEnqueueOrderUseCase>();
            services.AddScoped<IUpdateStatusKitchenQueueUseCase, UpdateStatusKitchenQueueUseCase>();
            services.AddScoped<IKitchenDequeueOrderUseCase, KitchenDequeueOrderUseCase>();

            #endregion

            return services;
        }
    }
}
