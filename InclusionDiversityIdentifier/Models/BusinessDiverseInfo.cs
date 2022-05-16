using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InclusionDiversityIdentifier.Models
{
    public class BusinessDiverseInfo
    {
        public int dunsNumId { get; set; }
        public List<string> urlLink { get; set; }
        public string businessName { get; set; }
        public bool isWomanOwned { get; set; }
        public string minorityOwnedDesc { get; set; }
    }
}
