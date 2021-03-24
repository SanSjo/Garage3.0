using System.ComponentModel.DataAnnotations;

namespace Garage3._0.Models
{
    public class VehicleType
    {
        [Key]
        public string Type { get; set; }
        public int Size { get; set; }
    }
}