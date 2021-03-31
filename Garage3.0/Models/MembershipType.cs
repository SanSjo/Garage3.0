using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

namespace Garage3.Models
{
    public class MembershipType
    {
        [Key]
        public int MembershiptTypeID { get; set; }        
        [Required]
        public string Type { get; set; }
        [Required]
        public int Discount { get; set; }
    }
}
