using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FormExample.Models
{
    public class SecondaryFinding
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int PrimaryFindingId { get; set; }

        public PrimaryFinding PrimaryFinding { get; set; }
    }
}
