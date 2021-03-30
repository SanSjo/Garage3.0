using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Garage3.Models.ViewModels
{
    public class RegisterNewVehicleViewModel
    {
        public string MemberID { get; set; }

        public string LicenseNumber { get; set; }

        public string VehicleType { get; set; }

        public string Color { get; set; }

        public string Brand { get; set; }

        public string Model { get; set; }

        public int NumberOfWheels { get; set; }

    }
}
