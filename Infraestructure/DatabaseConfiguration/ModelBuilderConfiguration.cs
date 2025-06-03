using Domain.Entities;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.DatabaseConfiguration
{
    public static class ModelBuilderConfiguration
    {

        public static void SeedDatabase(this ModelBuilder modelBuilder)
        {
            var adminEmail = new Email("admin@admin.com");
            var adminUserId = Guid.Parse("8833C9AF-A91A-411C-8B54-B554171287C0");

            var adminUser = new User(
                adminUserId,
                userName: "admin",
                email: adminEmail,
                passwordHash: "$2a$11$zMzsiOBzimzJNGVmnkdRleV34aqRNAtSe9Ys5lKqDZwN3hJS86jzK" // Password: "Admin123!"
            );

            modelBuilder.Entity<User>().HasData(adminUser);
        }
    }
}
