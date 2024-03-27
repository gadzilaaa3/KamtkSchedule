using KamtkSchedule.Infrastructure.Initialization;

namespace KamtkSchedule.Infrastracture.IntegrationTests.Base
{
    public class EnsureDatabaseTestFixture : IDisposable
    {
        public EnsureDatabaseTestFixture()
        {
            var configuration = TestHelpers.GetConfiguration();
            var context = TestHelpers.GetContext(configuration);
            DataInitializer.DropAndCreateDatabase(context);
            context.Dispose();
        }
        public void Dispose() { }
    }
}
