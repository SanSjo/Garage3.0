using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Garage3._0.Models
{
    public class ParkingSpace
    {
        [Key]
        public int PK { get; set; }

        public IList<Vehicle> Vehicles { get; set; }
    }
}
