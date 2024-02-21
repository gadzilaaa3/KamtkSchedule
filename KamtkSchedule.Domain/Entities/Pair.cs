using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KamtkSchedule.Domain.Entities
{
    public class Pair
    {
        public string Discipline { get; set; }
        public string WhoHasAPair { get; set; }
        public int PairNumber { get; set; }
        public string CabinetName { get; set; }
    }
}
