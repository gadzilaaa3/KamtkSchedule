using System.Collections.Generic;

namespace KamtkSchedule.Domain.Dtos.Parsers
{
    public class PairDto
    {
        public string Group { get; set; } = string.Empty;
        public IEnumerable<string> Teachers { get; set; } = new List<string>();
        public string Discipline { get; set; } = string.Empty;
        public int PairNumber { get; set; }
        public IEnumerable<string> Cabinets { get; set; } = new List<string>();
    }
}
