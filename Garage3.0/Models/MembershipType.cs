using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Garage3._0.Models
{
    public class MembershipType
    {
        [Key]
        public string Type { get; set; }
        public decimal Discount { get; set; }
    }
}
