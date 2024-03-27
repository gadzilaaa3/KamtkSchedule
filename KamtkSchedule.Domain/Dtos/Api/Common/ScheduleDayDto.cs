using KamtkSchedule.Domain.Dtos.Api.Base;
using System;
using System.Collections.Generic;

namespace KamtkSchedule.Domain.Dtos.Api.Common
{
    public class ScheduleDayDto : BaseEntityDto
    {
        public DateTime Date { get; set; }
        public IEnumerable<PairDto> Pairs { get; set; } = [];
        public DayOfWeek DayOfWeek { get; set; }
    }
}
