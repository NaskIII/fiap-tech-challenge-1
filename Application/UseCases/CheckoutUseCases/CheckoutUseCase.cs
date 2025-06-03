using Application.Dtos.CheckoutDtos.Request;
using Application.Dtos.KitchenDtos.Request;
using Application.Exceptions;
using Application.Interfaces;
using Domain.BaseInterfaces;
using Domain.Entities;
using Domain.Enums;
using Domain.RepositoryInterfaces;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.CheckoutUseCases
{
    public class CheckoutUseCase : ICheckoutUseCase
    {

        private readonly IPaymentGateway _paymentGateway;
        private readonly IOrderRepository _orderRepository;
        private readonly IPaymentRepository _paymentRepository;

        private IKitchenEnqueueOrderUseCase _kitchenEnqueueOrderUseCase;

        private readonly INotification _notification;
        private readonly ILogger<CheckoutUseCase> _logger;

        public CheckoutUseCase(
            IPaymentGateway paymentGateway,
            IOrderRepository orderRepository,
            IPaymentRepository paymentRepository,
            IKitchenEnqueueOrderUseCase kitchenEnqueueOrderUseCase,
            INotification notification,
            ILogger<CheckoutUseCase> logger)
        {
            _paymentGateway = paymentGateway;
            _orderRepository = orderRepository;
            _paymentRepository = paymentRepository;
            _kitchenEnqueueOrderUseCase = kitchenEnqueueOrderUseCase;
            _notification = notification;
            _logger = logger;
        }

        public async Task<Guid> ExecuteAsync(CheckoutRequest request)
        {
            Order? order = await _orderRepository.GetSingleAsync(x => x.OrderId == request.OrderId, x => x.OrderItems);
            if (order == null)
                throw new NotFoundException("Pedido não encontrado.");

            decimal total = order.GetTotal();

            Guid paymentId = await _paymentGateway.ProcessPaymentAsync(total, "QRCODE"); // !FIXME: EU ACHEI QUE ERA PARA FINGIR UMA INTEGRAÇÃO. SÓ PERCEBI QUE ERA PARA FAZER A INTEGRAÇÃO REAL QUANDO VI AS MENSAGENS NO DISCORD!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

            Payment payment = new Payment(order.OrderId, total, PaymentStatus.Completed, paymentId.ToString(), null);

            await _paymentRepository.BeginTransactionAsync();

            try
            {
                await _paymentRepository.AddAsync(payment);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao processar pagamento para o pedido {OrderId}", order.OrderId);
                await _paymentRepository.RollbackAsync();
                throw new ApplicationException("Erro ao processar pagamento. Tente novamente mais tarde.");
            }

            await _paymentRepository.CommitTransactionAsync();

            order.MarkAsReceived(payment.PaymentId);

            await _orderRepository.UpdateAsync(order);

            _logger.LogInformation("Pagamento processado com sucesso para o pedido {OrderId}", order.OrderId);

            ManageKitchenRequest manageKitchenRequest = new()
            {
                OrderId = order.OrderId,
                OrderStatus = order.OrderStatus
            };

            await _kitchenEnqueueOrderUseCase.ExecuteAsync(manageKitchenRequest);

            await _notification.NotifyKitchen(order.OrderStatus);
            await _notification.NotifyClient(order.OrderStatus);

            return paymentId;
        }
    }
}
