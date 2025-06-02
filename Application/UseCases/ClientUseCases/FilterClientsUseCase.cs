using Application.Dtos.Client.Request;
using Application.Dtos.Client.Response;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.RepositoryInterfaces;
using Domain.ValueObjects;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.ClientUseCases
{
    public class FilterClientsUseCase : IFilterClientsUseCase
    {
        private readonly IClientRepository _clientRepository;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public FilterClientsUseCase(IClientRepository clientRepository, ILogger<FilterClientsUseCase> logger, 
            IMapper mapper)
        {
            _clientRepository = clientRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<List<ClientSummaryResponse>> ExecuteAsync(FilterClientsRequest request)
        {
            Cpf? cpf = null;
            Email? email = null;

            try
            {
                if (!string.IsNullOrWhiteSpace(request.Cpf))
                    cpf = new Cpf(request.Cpf);

                if (!string.IsNullOrWhiteSpace(request.Email))
                    email = new Email(request.Email);
            }
            catch (Exception ex)
            {
                _logger.LogWarning("Invalid VO data in filter: {Message}", ex.Message);
                throw new ArgumentException("Dados inválidos para CPF ou Email no filtro.");
            }

            List<Client> clients = await _clientRepository.FilterClientAsync(request.Name, cpf?.Value, email?.Value);

            List<ClientSummaryResponse> clientSummaries = _mapper.Map<List<ClientSummaryResponse>>(clients);

            return clientSummaries;
        }
    }
}
