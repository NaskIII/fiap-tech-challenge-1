using Domain.Entities;
using Domain.ValueObjects;

namespace Domain.Test
{
    public class ClientTests
    {
        [Fact]
        public void CreateClient_WithValidData_ShouldSucceed()
        {
            var client = new Client(
                "João Silva",
                new Cpf("51471627802"),
                new Email("joao@email.com")
            );

            Assert.Equal("João Silva", client.Name);
            Assert.Equal(new Cpf("51471627802"), client.CPF);
            Assert.Equal(new Email("joao@email.com"), client.Email);
        }

        [Theory]
        [InlineData("")]
        [InlineData("     ")]
        public void UpdateName_WithInvalidName_ShouldThrow(string invalidName)
        {
            var client = new Client("João", new Cpf("51471627802"), new Email("email@test.com"));

            Assert.Throws<ArgumentException>(() => client.UpdateName(invalidName));
        }

        [Theory]
        [InlineData("")]
        [InlineData("emailsemarroba.com")]
        public void UpdateEmail_WithInvalidEmail_ShouldThrow(string invalidEmail)
        {
            var client = new Client("João", new Cpf("51471627802"), new Email("email@test.com"));

            Assert.Throws<ArgumentException>(() => client.UpdateEmail(new Email(invalidEmail)));
        }

        [Fact]
        public void UpdateEmail_WithValidEmail_ShouldUpdate()
        {
            var client = new Client("João", new Cpf("51471627802"), new Email("old@email.com"));

            client.UpdateEmail(new Email("novo@email.com"));

            Assert.Equal(new Email("novo@email.com"), client.Email);
        }

        [Fact]
        public void UpdateCPF_WithInvalidLength_ShouldThrow()
        {
            var client = new Client("João", new Cpf("51471627802"), new Email("email@test.com"));

            Assert.Throws<ArgumentException>(() => client.UpdateCPF(new Cpf("123")));
        }

        [Fact]
        public void UpdateCPF_WithValidCPF_ShouldUpdate()
        {
            var client = new Client("João", new Cpf("51471627802"), new Email("email@test.com"));

            client.UpdateCPF(new Cpf("20208472568"));

            Assert.Equal(new Cpf("20208472568"), client.CPF);
        }
    }
}
