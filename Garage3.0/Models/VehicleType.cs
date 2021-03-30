using System.ComponentModel.DataAnnotations;

namespace Garage3.Models
{
    public class VehicleType
    {
        [Key]
        public string Type { get; set; }
        public float Size { get; set; }
        public string imgSrc { get; set; }
    }
}