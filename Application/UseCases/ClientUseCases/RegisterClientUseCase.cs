using Application.Dtos.Client.Request;
using Application.Interfaces;
using Domain.Entities;
using Domain.RepositoryInterfaces;
using Domain.ValueObjects;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.ClientUseCases
{
    public class RegisterClientUseCase : IRegisterClientUseCase
    {
        private readonly IClientRepository _clientRepository;
        private readonly ILogger _logger;

        public RegisterClientUseCase(IClientRepository clientRepository, ILogger<RegisterClientUseCase> logger)
        {
            _clientRepository = clientRepository;
            _logger = logger;
        }

        public async Task<Guid> ExecuteAsync(RegisterClientRequest request)
        {
            Cpf cpf;
            Email email;

            try
            {
                cpf = new Cpf(request.Cpf);
                email = new Email(request.Email);
            }
            catch (Exception ex)
            {
                _logger.LogWarning("Invalid VO data: {Message}", ex.Message);
                throw new ArgumentException("Dados inválidos para CPF ou Email.");
            }

            var client = new Client(request.Name, cpf, email);

            if (await _clientRepository.ExistsAsync(x => x.CPF.Equals(client.CPF)))
                throw new ArgumentException("Já existe um cliente cadastrado com este CPF.");

            await _clientRepository.BeginTransactionAsync();

            client = await _clientRepository.AddAsync(client);
            
            if (client == null)
            {
                await _clientRepository.RollbackAsync();
                _logger.LogError("Erro ao cadastrar cliente: {ClientName}", request.Name);
                throw new InvalidOperationException("Não foi possível cadastrar o cliente.");
            }

            await _clientRepository.CommitTransactionAsync();

            return client.ClientId;
        }
    }

}

