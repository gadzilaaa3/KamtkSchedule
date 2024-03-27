using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace KamtkSchedule.Infrastructure.Data
{
    public class ApplicationDbContextFactory
        : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            string connectionString = 
                @"Data Source=(localdb)\mssqllocaldb;Integrated Security=true;Initial Catalog=KamtkSchedule";
            optionsBuilder.UseSqlServer(connectionString);
            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}
