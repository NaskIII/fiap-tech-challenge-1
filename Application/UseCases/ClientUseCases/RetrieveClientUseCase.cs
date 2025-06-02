using Application.Dtos.Client.Response;
using Application.Exceptions;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.RepositoryInterfaces;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.ClientUseCases
{
    public class RetrieveClientUseCase : IRetrieveClientUseCase
    {

        private readonly IClientRepository _clientRepository;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public RetrieveClientUseCase(IClientRepository clientRepository, ILogger<RetrieveClientUseCase> logger,
            IMapper mapper)
        {
            _clientRepository = clientRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<ClientResponse> ExecuteAsync(Guid request)
        {
            Client? client = await _clientRepository.GetByIdAsync(request);

            if (client == null)
            {
                _logger.LogWarning("Client with ID {ClientId} not found.", request);
                throw new NotFoundException($"Client with ID {request} not found.");
            }

            ClientResponse response = _mapper.Map<ClientResponse>(client);

            return response;
        }
    }
}
