using Application.Dtos.CheckoutDtos.Request;
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
        private readonly ILogger<CheckoutUseCase> _logger;

        public CheckoutUseCase(
            IPaymentGateway paymentGateway,
            IOrderRepository orderRepository,
            IPaymentRepository paymentRepository,
            ILogger<CheckoutUseCase> logger)
        {
            _paymentGateway = paymentGateway;
            _orderRepository = orderRepository;
            _logger = logger;
        }

        public async Task<Guid> ExecuteAsync(CheckoutRequest request)
        {
            Order? order = await _orderRepository.GetByIdAsync(request.OrderId);
            if (order == null)
                throw new NotFoundException("Pedido não encontrado.");

            decimal total = order.GetTotal();

            Guid paymentId = await _paymentGateway.ProcessPaymentAsync(total, "QRCODE"); // !FIXME: EU ACHEI QUE ERA PARA FINGIR UMA INTEGRAÇÃO. SÓ PERCEBI QUE ERA PARA FAZER A INTEGRAÇÃO REAL QUANDO VI O AS MENSAGENS NO DISCORD!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

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

            return paymentId;
        }
    }
}
