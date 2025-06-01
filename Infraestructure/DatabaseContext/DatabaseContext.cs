using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infraestructure.DatabaseContext
{
    internal class DatabaseContext : DbContext
    {

        public readonly IConfiguration _configuration;

        public DatabaseContext(DbContextOptions options, IConfiguration configuration) : base(options)
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
            //modelBuilder.SeedDatabase();

            //this.ConfigureModel(modelBuilder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}