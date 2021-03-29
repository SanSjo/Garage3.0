using System.ComponentModel.DataAnnotations;

namespace Garage3.Models
{
    public class VehicleType
    {
        [Key]
        public string Type { get; set; }
        public double Size { get; set; }
    }
}