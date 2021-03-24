using System.ComponentModel.DataAnnotations;

namespace Garage3._0.Models
{
    public class ParkingSpot
    {
        [Key]
        public int Id { get; set; }

        public int Number { get; set; }

        public string Name { get; set; }

        public Vehicle Vehicle { get; set; }
    }
}