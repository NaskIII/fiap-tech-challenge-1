using Application.Dtos.KitchenDtos.Request;
using Application.Exceptions;
using Application.Interfaces;
using Domain.BaseInterfaces;
using Domain.Entities;
using Domain.Enums;
using Domain.RepositoryInterfaces;

namespace Application.UseCases.KitchenQueueUseCases
{
    public class UpdateStatusKitchenQueueUseCase : IUpdateStatusKitchenQueueUseCase
    {

        private readonly IKitchenQueueRepository _kitchenQueueRepository;
        private readonly IOrderRepository _orderRepository;

        private readonly IKitchenDequeueOrderUseCase _kitchenDequeueOrderUseCase;

        private readonly INotification _notification;

        public UpdateStatusKitchenQueueUseCase(
            IKitchenQueueRepository kitchenQueueRepository,
            IOrderRepository orderRepository,
            IKitchenDequeueOrderUseCase kitchenDequeueOrderUseCase,
            INotification notification)
        {
            _kitchenQueueRepository = kitchenQueueRepository;
            _orderRepository = orderRepository;
            _kitchenDequeueOrderUseCase = kitchenDequeueOrderUseCase;
            _notification = notification;
        }

        public async Task ExecuteAsync(ManageKitchenRequest request)
        {
            Order? order = await _orderRepository.GetByIdAsync(request.OrderId);
            if (order == null)
                throw new NotFoundException($"Order with ID {request.OrderId} not found.");

            KitchenQueue? kitchenQueue = await _kitchenQueueRepository.GetSingleAsync(x => x.OrderId == request.OrderId);
            if (kitchenQueue == null)
                throw new NotFoundException($"Order with ID {request.OrderId} not found.");

            kitchenQueue.ChangeOrderStatus(request.OrderStatus);

            await _kitchenQueueRepository.BeginTransactionAsync();

            try
            {
                await _kitchenQueueRepository.UpdateAsync(kitchenQueue);
            }
            catch (Exception ex)
            {
                await _kitchenQueueRepository.RollbackAsync();
                throw new ApplicationException("An error occurred while updating the kitchen queue status.", ex);
            }

            await _kitchenQueueRepository.CommitTransactionAsync();

            await _notification.NotifyClient(request.OrderStatus);

            if (order.OrderStatus == OrderStatus.Completed)
            {
                ManageKitchenRequest manageKitchenRequest = new()
                {
                    OrderId = order.OrderId,
                    OrderStatus = order.OrderStatus
                };

                await _kitchenDequeueOrderUseCase.ExecuteAsync(manageKitchenRequest);
            }
        }
    }
}
