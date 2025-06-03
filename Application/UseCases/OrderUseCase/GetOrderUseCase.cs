using Application.Dtos.OrderDtos.Response;
using Application.Exceptions;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.RepositoryInterfaces;

namespace Application.UseCases.OrderUseCase
{
    public class GetOrderUseCase : IGetOrderUseCase
    {

        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public GetOrderUseCase(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<OrderResponse> ExecuteAsync(Guid request)
        {
            Order? order = await _orderRepository.GetSingleAsync(x => x.OrderId == request, "OrderItems.Product", "Client");
            if (order == null)
                throw new NotFoundException($"Order with ID {request} not found.");

            return _mapper.Map<OrderResponse>(order);
        }
    }
}
