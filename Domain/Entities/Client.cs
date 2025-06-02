using Domain.ValueObjects;

namespace Domain.Entities
{
    public class Client
    {
        public Guid ClientId { get; private set; }
        public string Name { get; private set; }
        public Cpf CPF { get; private set; }
        public Email Email { get; private set; }
        public DateTime RegisterDate { get; private set; }

        protected Client()
        {
            Name = string.Empty;
            CPF = default!;
            Email = default!;
        }

        public Client(string name, Cpf cpf, Email email)
        {
            Name = string.Empty;
            CPF = default!;
            Email = default!;

            UpdateName(name);
            UpdateCPF(cpf);
            UpdateEmail(email);
        }

        public void UpdateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Nome inválido");
            Name = name;
        }

        public void UpdateEmail(Email email)
        {
            Email = email ?? throw new ArgumentNullException(nameof(email));
        }

        public void UpdateCPF(Cpf cpf)
        {
            CPF = cpf ?? throw new ArgumentNullException(nameof(cpf));
        }
    }
}

