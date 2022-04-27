using CloudEngineering.CodeOps.Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DigitalTwins.Management.Application.Data
{
    public sealed class ApplicationContextDesignFactory : IDesignTimeDbContextFactory<ApplicationContext>
    {
        public ApplicationContext CreateDbContext(string[] args)
        {
            const string connStr = "User ID=postgres;Password=local;Host=localhost;Port=5432;Database=postgres";
            var connection = new Npgsql.NpgsqlConnection(connStr);

            connection.Open();

            var optionsBuilder = new DbContextOptionsBuilder<EntityContext>()
                .UseNpgsql(connection);

            return new ApplicationContext(optionsBuilder.Options);
        }
    }
}
