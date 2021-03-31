using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.AspNetCore.Http;

namespace Garage3.Models
{
    public class VehicleType
    {
        [Key]
        public int TypeID { get; set; }
        [Required]        
        public string Type { get; set; }
        [Required]
        public float Size { get; set; }
        public string imgSrc { get; set; }
    }
}