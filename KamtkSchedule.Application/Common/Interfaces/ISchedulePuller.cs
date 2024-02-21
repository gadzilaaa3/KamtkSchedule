using KamtkSchedule.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KamtkSchedule.Application.Common.Interfaces
{
    public interface ISchedulePuller
    {
        public IEnumerable<Schedule> GetSchedulesFor(string groupName);

        public IEnumerable<Schedule> GetSchedulesForAll();
    }
}
