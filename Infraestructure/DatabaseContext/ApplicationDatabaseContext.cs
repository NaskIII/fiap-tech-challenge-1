﻿using Domain.Entities;
using Infraestructure.DatabaseConfiguration;
using Infraestructure.EntitiesConfiguration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infraestructure.DatabaseContext
{
    public class ApplicationDatabaseContext : DbContext
    {

        public readonly IConfiguration _configuration;

        public DbSet<Order> Orders => Set<Order>();
        public DbSet<Payment> Payments => Set<Payment>();
        public DbSet<Client> Clients => Set<Client>();
        public DbSet<Product> Products => Set<Product>();
        public DbSet<ProductCategory> ProductCategories => Set<ProductCategory>();
        public DbSet<OrderItem> OrderItems => Set<OrderItem>();

        public ApplicationDatabaseContext(DbContextOptions options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        //protected abstract override void OnConfiguring(DbContextOptionsBuilder options);
        //protected abstract void ConfigureModel(ModelBuilder modelBuilder);

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Precision();
            //modelBuilder.DateTimeUTC();
            //modelBuilder.ConvertFields();
            //modelBuilder.SelfRelationship();
            //modelBuilder.ManyToManyRelationship();
            //modelBuilder.OneToManyRelationship();
            //modelBuilder.OneToOneRelationship();
            //modelBuilder.Indexes();
            modelBuilder.SeedDatabase();

            //this.ConfigureModel(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDatabaseContext).Assembly);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}