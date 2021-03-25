using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Garage3.Models
{
    public class MembersViewModel
    {
        public int MemberID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int NrOfVehicles { get; set; }
        public string Search { get; set; }
    }
}
