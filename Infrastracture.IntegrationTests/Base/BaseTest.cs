using KamtkSchedule.Infrastructure.Data;
using Microsoft.Extensions.Configuration;

namespace KamtkSchedule.Infrastracture.IntegrationTests.Base
{
    public abstract class BaseTest : IDisposable
    {
        protected readonly IConfiguration Configuration;
        protected readonly ApplicationDbContext Context;
        protected BaseTest()
        {
            Configuration = TestHelpers.GetConfiguration();
            Context = TestHelpers.GetContext(Configuration);
        }
        public virtual void Dispose()
        {
            Context.Dispose();
        }
    }
}
