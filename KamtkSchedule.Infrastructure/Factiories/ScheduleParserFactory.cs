using KamtkSchedule.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KamtkSchedule.Infrastructure.Factiories
{
    public abstract class ScheduleParserFactory
    {
        public abstract IScheduleParser CreateStudentScheduleParserBuildingA();
        public abstract IScheduleParser CreateStudentScheduleParserBuildingB();
        public abstract IScheduleParser CreateTeacherScheduleParserBuildingA();
        public abstract IScheduleParser CreateTeacherScheduleParserBuildingB();
        public abstract IScheduleParser CreateDefaultScheduleParser();
    }
}
