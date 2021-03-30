using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Garage3.Models
{
    public class MembershipType
    {
        [Key]
        public int MembershiptTypeID { get; set; }        
        public string Type { get; set; }
        // TODO: What is a good value to have here?                               
        public decimal Discount { get; set; }
    }
}
