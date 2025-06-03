using Application.Dtos.Client.Response;
using Application.Dtos.OrderDtos.Request;
using Application.Dtos.OrderDtos.Response;
using Application.Dtos.OrderItemDtos.Response;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappers
{
    public class OrderMappers : Profile
    {

        public OrderMappers()
        {
            CreateMap<Order, OrderResponse>()
                .ForMember(dest => dest.Client, opt => opt.MapFrom((src, destiantion) =>
                {
                    if (src.Client == null) return null;
                    return new ClientResponse
                    {
                        ClientId = src.Client.ClientId,
                        Name = src.Client.Name,
                        CPF = src.Client.CPF.Value,
                        Email = src.Client.Email.Value,
                        RegisterDate = src.Client.RegisterDate
                    };
                }))
                .ForMember(dest => dest.OrderItems, opt => opt.MapFrom((src, destination) =>
                {
                    return src.OrderItems.Select(item => new OrderItemResponse
                    {
                        ProductId = item.ProductId,
                        ProductName = item.Product.ProductName.Value,
                        Quantity = item.Quantity,
                        UnitPrice = item.UnitPrice
                    }).ToList();
                }))
                .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.GetTotal()));

        }
    }
}
