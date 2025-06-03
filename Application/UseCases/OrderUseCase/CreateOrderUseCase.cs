using Application.Dtos.OrderDtos.Request;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Domain.RepositoryInterfaces;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.OrderUseCase
{
    public class CreateOrderUseCase : ICreateOrderUseCase
    {

        private readonly IOrderRepository _orderRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IProductRespository _productRepository;
        private readonly ILogger<CreateOrderUseCase> _logger;

        public CreateOrderUseCase(
            IOrderRepository orderRepository,
            IClientRepository clientRepository,
            IProductRespository productRepository,
            ILogger<CreateOrderUseCase> logger)
        {
            _orderRepository = orderRepository;
            _clientRepository = clientRepository;
            _productRepository = productRepository;
            _logger = logger;
        }

        public async Task<Guid> ExecuteAsync(OrderRequest request)
        {
            Client? client = null;
            if (request.ClientId.HasValue)
            {
                client = await _clientRepository.GetByIdAsync(request.ClientId.Value);
                if (client == null)
                {
                    _logger.LogError("Client with ID {ClientId} not found.", request.ClientId);
                    throw new ArgumentException($"Client with ID {request.ClientId} not found.");
                }
            }

            if (request.OrderItems == null || !request.OrderItems.Any())
            {
                _logger.LogError("Order must have at least one item.");
                throw new ArgumentException("Order must have at least one item.");
            }

            var order = new Order(client?.ClientId);

            foreach (var itemDto in request.OrderItems)
            {
                var product = await _productRepository.GetByIdAsync(itemDto.ProductId);
                if (product == null)
                {
                    _logger.LogError("Product with ID {ProductId} not found.", itemDto.ProductId);
                    throw new ArgumentException($"Product with ID {itemDto.ProductId} not found.");
                }

                if (itemDto.Quantity <= 0)
                {
                    _logger.LogError("Quantity must be greater than zero for product {ProductId}.", itemDto.ProductId);
                    throw new ArgumentException("Quantity must be greater than zero.");
                }


                order.AddItem(product, itemDto.Quantity);
            }

            await _orderRepository.BeginTransactionAsync();

            try
            {
                await _orderRepository.AddAsync(order);
            }
            catch (Exception ex)
            {
                await _orderRepository.RollbackAsync();
                _logger.LogError(ex, "Error creating order.");
                throw;
            }

            await _orderRepository.CommitTransactionAsync();

            _logger.LogInformation("Order {OrderId} created successfully.", order.OrderId);

            return order.OrderId;
        }
    }
}
