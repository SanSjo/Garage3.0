using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Garage3.Models
{
    public class ParkingSpace
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ParkingSpaceID { get; set; }
        public int Number { get; set; }
        public string Name { get; set; }
        public IList<Vehicle> Vehicle { get; set; }
    }
}
