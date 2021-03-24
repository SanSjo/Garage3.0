using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Garage3._0.Models
{
    public class BookedTimes
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PK { get; set; }

        public DateTime BookedTime { get; set; }
        public IList<ParkingSpace> SpaceOccupied { get; set; }
        public Member Member { get; set; }
    }
}
