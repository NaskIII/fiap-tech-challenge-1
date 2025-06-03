using Application.Dtos.KitchenDtos.Request;
using Application.Exceptions;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Domain.RepositoryInterfaces;

namespace Application.UseCases.KitchenQueueUseCases
{
    public class KitchenDequeueOrderUseCase : IKitchenDequeueOrderUseCase
    {
        private readonly IKitchenQueueRepository _kitchenQueueRepository;
        private readonly IOrderRepository _orderRepository;

        public KitchenDequeueOrderUseCase(IKitchenQueueRepository kitchenQueueRepository, IOrderRepository orderRepository)
        {
            _kitchenQueueRepository = kitchenQueueRepository;
            _orderRepository = orderRepository;
        }

        public async Task ExecuteAsync(ManageKitchenRequest request)
        {
            Order? order = await _orderRepository.GetByIdAsync(request.OrderId);
            if (order == null)
                throw new NotFoundException($"Order with ID {request.OrderId} not found.");

            KitchenQueue? kitchenQueue = await _kitchenQueueRepository.GetSingleAsync(x => x.OrderId == request.OrderId);
            if (kitchenQueue == null)
                throw new NotFoundException($"Order with ID {request.OrderId} not found.");

            order.ChangeStatus(OrderStatus.Completed);

            await _kitchenQueueRepository.BeginTransactionAsync();

            try
            {
                await _kitchenQueueRepository.DeleteAsync(kitchenQueue);
            }
            catch (Exception ex)
            {
                await _kitchenQueueRepository.RollbackAsync();
                throw new ApplicationException("An error occurred while denqueuing the order.", ex);
            }

            try
            {
                await _orderRepository.UpdateAsync(order);
            }
            catch (Exception ex)
            {
                await _kitchenQueueRepository.RollbackAsync();
                throw new ApplicationException("An error occurred while updating the order status.", ex);
            }

            await _kitchenQueueRepository.CommitTransactionAsync();
        }
    }
}
