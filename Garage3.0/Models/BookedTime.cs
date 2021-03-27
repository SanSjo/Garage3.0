using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Garage3.Models
{
    public class Booking
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BookingId { get; set; }

        [Display(Name = "Booked Time")]
        public DateTime BookedTime { get; set; }

        [Display(Name = "Booked by")]
        public Member BookedBy { get; set; }
    }
}
