using Application.Dtos.Client.Request;
using Application.UseCases;
using Domain.Entities;
using Domain.RepositoryInterfaces;
using Domain.ValueObjects;
using Microsoft.Extensions.Logging;
using Moq;
using System.Linq.Expressions;

namespace Application.Test
{
    public class RegisterClientUseCaseTests
    {
        [Fact]
        public async Task ExecuteAsync_ValidRequest_ShouldReturnClientId()
        {
            var request = new RegisterClientRequest
            {
                Name = "João Silva",
                Cpf = "51471627802",
                Email = "joao@email.com"
            };

            var clientId = Guid.NewGuid();
            var client = new Client(request.Name, new Cpf(request.Cpf), new Email(request.Email));

            var clientRepositoryMock = new Mock<IClientRepository>();
            var loggerMock = new Mock<ILogger<RegisterClientUseCase>>();

            clientRepositoryMock.Setup(r => r.ExistsAsync(It.IsAny<Expression<Func<Client, bool>>>())).ReturnsAsync(false);

            clientRepositoryMock.Setup(r => r.BeginTransactionAsync()).Returns(Task.CompletedTask);

            clientRepositoryMock.Setup(r => r.AddAsync(It.IsAny<Client>()))
                .ReturnsAsync((Client c) =>
                {
                    typeof(Client).GetProperty("ClientId").SetValue(c, clientId);
                    return c;
                });

            clientRepositoryMock.Setup(r => r.CommitTransactionAsync()).Returns(Task.CompletedTask);

            clientRepositoryMock.Setup(r => r.RollbackAsync()).Returns(Task.CompletedTask);

            var useCase = new RegisterClientUseCase(clientRepositoryMock.Object, loggerMock.Object);

            var result = await useCase.ExecuteAsync(request);

            Assert.NotEqual(Guid.Empty, result);
            Assert.Equal(clientId, result);

            clientRepositoryMock.Verify(r => r.ExistsAsync(It.IsAny<Expression<Func<Client, bool>>>()), Times.Once);
            clientRepositoryMock.Verify(r => r.BeginTransactionAsync(), Times.Once);
            clientRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Client>()), Times.Once);
            clientRepositoryMock.Verify(r => r.CommitTransactionAsync(), Times.Once);
            clientRepositoryMock.Verify(r => r.RollbackAsync(), Times.Never);
        }

        [Fact]
        public async Task ExecuteAsync_DuplicateCpf_ShouldThrowArgumentException()
        {
            var request = new RegisterClientRequest
            {
                Name = "João Silva",
                Cpf = "51471627802",
                Email = "joao@email.com"
            };

            var clientRepositoryMock = new Mock<IClientRepository>();
            var loggerMock = new Mock<ILogger<RegisterClientUseCase>>();

            clientRepositoryMock.Setup(r => r.ExistsAsync(It.IsAny<Expression<Func<Client, bool>>>()))
                .ReturnsAsync(true);

            var useCase = new RegisterClientUseCase(clientRepositoryMock.Object, loggerMock.Object);

            var ex = await Assert.ThrowsAsync<ArgumentException>(() => useCase.ExecuteAsync(request));
            Assert.Equal("Já existe um cliente cadastrado com este CPF.", ex.Message);

            clientRepositoryMock.Verify(r => r.ExistsAsync(It.IsAny<Expression<Func<Client, bool>>>()), Times.Once);
            clientRepositoryMock.Verify(r => r.BeginTransactionAsync(), Times.Never);
        }

        [Fact]
        public async Task ExecuteAsync_InvalidCpfOrEmail_ShouldThrowArgumentException()
        {
            var request = new RegisterClientRequest
            {
                Name = "João Silva",
                Cpf = "invalidcpf",
                Email = "invalidemail"
            };

            var clientRepositoryMock = new Mock<IClientRepository>();
            var loggerMock = new Mock<ILogger<RegisterClientUseCase>>();

            var useCase = new RegisterClientUseCase(clientRepositoryMock.Object, loggerMock.Object);

            var ex = await Assert.ThrowsAsync<ArgumentException>(() => useCase.ExecuteAsync(request));
            Assert.Equal("Dados inválidos para CPF ou Email.", ex.Message);

            loggerMock.Verify(l => l.Log(
                LogLevel.Warning,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString()!.Contains("Invalid VO data")),
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
                Times.Once);


            clientRepositoryMock.Verify(r => r.ExistsAsync(It.IsAny<Expression<Func<Client, bool>>>()), Times.Never);
            clientRepositoryMock.Verify(r => r.BeginTransactionAsync(), Times.Never);
        }

        [Fact]
        public async Task ExecuteAsync_AddAsyncReturnsNull_ShouldRollbackAndThrow()
        {
            var request = new RegisterClientRequest
            {
                Name = "João Silva",
                Cpf = "51471627802",
                Email = "joao@email.com"
            };

            var clientRepositoryMock = new Mock<IClientRepository>();
            var loggerMock = new Mock<ILogger<RegisterClientUseCase>>();

            clientRepositoryMock.Setup(r => r.ExistsAsync(It.IsAny<Expression<Func<Client, bool>>>())).ReturnsAsync(false);
            clientRepositoryMock.Setup(r => r.BeginTransactionAsync()).Returns(Task.CompletedTask);
            clientRepositoryMock.Setup(r => r.AddAsync(It.IsAny<Client>())).ReturnsAsync((Client?)null);
            clientRepositoryMock.Setup(r => r.RollbackAsync()).Returns(Task.CompletedTask);

            var useCase = new RegisterClientUseCase(clientRepositoryMock.Object, loggerMock.Object);

            var ex = await Assert.ThrowsAsync<InvalidOperationException>(() => useCase.ExecuteAsync(request));
            Assert.Equal("Não foi possível cadastrar o cliente.", ex.Message);

            clientRepositoryMock.Verify(r => r.RollbackAsync(), Times.Once);
            clientRepositoryMock.Verify(r => r.CommitTransactionAsync(), Times.Never);

            loggerMock.Verify(l => l.Log(
                LogLevel.Error,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString()!.Contains(request.Name)),
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
                Times.Once);
        }
    }

}
