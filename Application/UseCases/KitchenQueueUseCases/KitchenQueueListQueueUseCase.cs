using Application.Dtos.KitchenDtos.Response;
using Application.Dtos.OrderDtos.Response;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.RepositoryInterfaces;

namespace Application.UseCases.KitchenQueueUseCases
{
    public class KitchenQueueListQueueUseCase : IKitchenQueueListQueue
    {

        private readonly IKitchenQueueRepository _kitchenQueueRepository;
        private readonly IMapper _mapper;

        public KitchenQueueListQueueUseCase(IKitchenQueueRepository kitchenQueueRepository, IMapper mapper)
        {
            _kitchenQueueRepository = kitchenQueueRepository;
            _mapper = mapper;
        }

        public async Task<List<KitchenQueueResponse>> ExecuteAsync(object? request)
        {
            List<KitchenQueue> kitchenQueues = await _kitchenQueueRepository.ListKitchenQueue();

            List<KitchenQueueResponse> kitchenQueueResponses = kitchenQueues
                .Select(kq => new KitchenQueueResponse
                {
                    KitchenQueueId = kq.KitchenQueueId,
                    Order = _mapper.Map<OrderResponse>(kq.Order),
                    EnqueuedAt = kq.EnqueuedAt
                })
                .ToList();

            return kitchenQueueResponses;
        }
    }
}
