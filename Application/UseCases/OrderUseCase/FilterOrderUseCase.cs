using Application.Dtos.OrderDtos.Request;
using Application.Dtos.OrderDtos.Response;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.RepositoryInterfaces;

namespace Application.UseCases.OrderUseCase
{
    public class FilterOrderUseCase : IFilterOrderUseCase
    {

        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public FilterOrderUseCase(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<List<OrderResponse>> ExecuteAsync(FilterOrderRequest request)
        {
            List<Order> orders = await _orderRepository.FilterOrders(request.OrderDate, request.ClientId, request.OrderStatus);

            return _mapper.Map<List<OrderResponse>>(orders);
        }
    }
}
