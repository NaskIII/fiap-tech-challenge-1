using Domain.ValueObjects;

namespace Domain.Entities
{
    public class User
    {

        public Guid UserId { get; private set; }
        public string UserName { get; private set; } = string.Empty;
        public Email Email { get; private set; }
        public string PasswordHash { get; private set; } = string.Empty;

        public User()
        {
            Email = default!;
        }

        public User(string userName, Email email, string passwordHash)
        {
            if (string.IsNullOrWhiteSpace(userName))
                throw new ArgumentException("Nome de usuário não pode ser vazio.");

            if (string.IsNullOrWhiteSpace(passwordHash))
                throw new ArgumentException("Hash da senha não pode ser vazio.");

            UserName = userName;
            Email = email;
            PasswordHash = passwordHash;
        }

        public User(Guid userId, string userName, Email email, string passwordHash)
        {
            if (string.IsNullOrWhiteSpace(userName))
                throw new ArgumentException("Nome de usuário não pode ser vazio.");

            if (string.IsNullOrWhiteSpace(passwordHash))
                throw new ArgumentException("Hash da senha não pode ser vazio.");

            UserId = userId;
            UserName = userName;
            Email = email;
            PasswordHash = passwordHash;
        }

        public void ChangePassword(string newPasswordHash)
        {
            if (string.IsNullOrWhiteSpace(newPasswordHash))
                throw new ArgumentException("Hash da nova senha inválido.");

            PasswordHash = newPasswordHash;
        }
    }
}
