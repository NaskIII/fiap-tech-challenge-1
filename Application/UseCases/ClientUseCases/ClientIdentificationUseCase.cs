using Application.Dtos.ClientDtos.Request;
using Application.Dtos.ClientDtos.Response;
using Application.Exceptions;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.RepositoryInterfaces;
using Domain.ValueObjects;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.ClientUseCases
{
    public class ClientIdentificationUseCase : IClientIdentificationUseCase
    {

        private readonly IClientRepository _clientRepository;
        private readonly ITokenService _tokenService;

        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public ClientIdentificationUseCase(IClientRepository clientRepository, ITokenService tokenService,
            ILogger<ClientIdentificationUseCase> logger, IMapper mapper)
        {
            _clientRepository = clientRepository;
            _tokenService = tokenService;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<AuthenticatedClientResponse> ExecuteAsync(ClientIdentificationRequest request)
        {
            Cpf? cpf = null;

            try
            {
                cpf = new Cpf(request.Cpf);
            }
            catch (Exception ex)
            {
                _logger.LogWarning("Invalid VO data in filter: {Message}", ex.Message);
                throw new ArgumentException("Dados inválidos para CPF.");
            }

            Client? client = await _clientRepository.GetClientByCpfAsync(cpf.Value);
            if (client == null)
            {
                _logger.LogWarning("Client not found for CPF: {Cpf}", cpf.Value);
                throw new NotFoundException("Cliente não encontrado.");
            }

            AuthenticatedClientResponse response = _mapper.Map<AuthenticatedClientResponse>(client);

            response.Token = _tokenService.GenerateToken(client.ClientId, client.CPF.Value);

            _logger.LogInformation("Client identified successfully: {ClientId}", client.ClientId);

            return response;
        }
    }
}
