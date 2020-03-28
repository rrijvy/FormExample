using System.Collections.Generic;

namespace FormExample.Models
{
    public class Patient
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<PrimaryFinding> PrimaryFindings { get; set; }
    }
}
