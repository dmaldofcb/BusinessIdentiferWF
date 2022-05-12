using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InclusionDiversityIdentifier.Models
{
    public class Business
    {
        [Key]
        public int dunsNumId { get; set; }
        public string dunsName { get; set; }
        public string county { get; set; }
        public string streetAddress { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string zipCode { get; set; }
        public string phoneNumber { get; set; }
        public string executiveContact { get; set; }
        public string executiveContact2 { get; set; }
        public bool isWomanOwned { get; set; }
        public string minorityOwnedDesc { get; set; }
    }
}
