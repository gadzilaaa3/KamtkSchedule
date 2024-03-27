using KamtkSchedule.Application.Common.Interfaces.Repositories;
using KamtkSchedule.Domain.Dtos;
using KamtkSchedule.Infrastructure.Data;
using KamtkSchedule.Infrastructure.Parsers.Factiories;
using KamtkSchedule.Infrastructure.Pullers;
using KamtkSchedule.Infrastructure.Repositories.Database;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KamtkSchedule.Infrastracture.IntegrationTests.IntegrationTests
{
    public class UnitTestForRepositories
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public UnitTestForRepositories()
        {
            _context = new ApplicationDbContextFactory().CreateDbContext([]);

            _configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
        }
    }
}
