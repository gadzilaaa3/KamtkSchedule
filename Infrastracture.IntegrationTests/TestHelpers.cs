using KamtkSchedule.Infrastructure.Data;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace KamtkSchedule.Infrastracture.IntegrationTests
{
    public static class TestHelpers
    {
        public static IConfiguration GetConfiguration() =>
            new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", true, true)
            .Build();

        public static ApplicationDbContext GetContext(
            IConfiguration configuration)
        {
            var optionsBuilder = new DbContextOptionsBuilder
                <ApplicationDbContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(connectionString,
                sqlOptions => sqlOptions.EnableRetryOnFailure());
            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}
