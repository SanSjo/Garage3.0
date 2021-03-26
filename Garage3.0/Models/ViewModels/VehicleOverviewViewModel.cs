using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Garage3.Models.ViewModels
{
    public class VehicleOverviewViewModel
    {
        public int VehicleID { get; set; }
        public string Owner { get; set; }
        public string MembershipType { get; set; }
        public string VehicleType { get; set; }
        public string LicenseNumber { get; set; }
        public TimeSpan TimeParked { get; set; }
    }
}
