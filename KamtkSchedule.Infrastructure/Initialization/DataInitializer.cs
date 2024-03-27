using KamtkSchedule.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace KamtkSchedule.Infrastructure.Initialization
{
    public static class DataInitializer
    {
        public static void DropAndCreateDatabase(ApplicationDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.Migrate();
        }
    }
}
