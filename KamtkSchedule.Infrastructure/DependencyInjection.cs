using KamtkSchedule.Application.Common.Interfaces;
using KamtkSchedule.Application.Common.Interfaces.Repositories;
using KamtkSchedule.Infrastructure.Data;
using KamtkSchedule.Infrastructure.Parsers.Factiories;
using KamtkSchedule.Infrastructure.Pullers;
using KamtkSchedule.Infrastructure.Repositories.Database;
using KamtkSchedule.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DependencyInjection
    {
        private const int retryCount = 3;

        public static IServiceCollection AddInfrastructureServices(
            this IServiceCollection services, IConfiguration configuration) 
        {
            var connectionString = 
                configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<ApplicationDbContext>((sp, options) =>
            {
                options.UseSqlServer(connectionString, options =>
                {
                    options.EnableRetryOnFailure(retryCount);
                });
            });

            // Repositories
            services.AddTransient<ICabinetRepository, CabinetRepository>();
            services.AddTransient<IDisciplineRepository, DisciplineRepository>();
            services.AddTransient<IGroupRepository, GroupRepository>();
            services.AddTransient<IGroupScheduleRepository, GroupScheduleRepository>();
            services.AddTransient<IPairRepository, PairRepository>();
            services.AddTransient<IScheduleDayRepository, ScheduleDayRepository>();
            services.AddTransient<ITeacherRepository, TeacherRepository>();
            services.AddTransient<IWeeklyScheduleRepository, WeeklyScheduleRepository>();

            services.AddTransient<IScheduleParserFactory, HtmlScheduleParserFactory>();
            services.AddTransient<ISchedulePuller, SchedulePuller>();
            services.AddTransient<ScheduleKeeper>();

            return services;
        }
    }
}
