using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.AspNetCore.Http;

namespace Garage3.Models
{
    public class VehicleType
    {
        [Key]        
        public string Type { get; set; }
        
        // BUG: Localisation error makes it so that you cannot enter decimal values when app is running
        [Required]
        public float Size { get; set; }

        // TODO: Add possibility to upload image and add src to this property
        public string imgSrc { get; set; }
    }
}