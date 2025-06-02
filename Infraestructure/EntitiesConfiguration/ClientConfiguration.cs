using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infraestructure.EntitiesConfiguration
{
    public class ClientConfiguration : IEntityTypeConfiguration<Client>
    {

        public void Configure(EntityTypeBuilder<Client> builder)
        {
            var cpfConverter = new ValueConverter<Cpf, string>(
                cpf => cpf.Value,
                value => new Cpf(value)
            );

            var emailConverter = new ValueConverter<Email, string>(
                email => email.Value,
                value => new Email(value)
            );

            builder.HasKey(x => x.ClientId);
            builder.Property(x => x.ClientId)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Email).IsRequired().HasMaxLength(100);
            builder.Property(x => x.RegisterDate).IsRequired();

            builder.Property(c => c.CPF)
                .HasConversion(cpfConverter)
                .HasMaxLength(11)
                .IsRequired();

            builder.Property(c => c.Email)
                .HasConversion(emailConverter)
                .HasMaxLength(100)
                .IsRequired();
            

            builder.HasIndex(x => x.CPF).IsUnique();
        }
    }

}
