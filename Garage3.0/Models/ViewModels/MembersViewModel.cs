using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Garage3.Models.ViewModels
{
    public class MembersViewModel
    {
        public string PersonalIdentityNumber { get; set; }
        public DateTime Joined { get; set; }
        public DateTime? ExtendenMemberShipEndDate { get; set; }                
        public int MemberID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int NrOfVehicles { get; set; }
        public string Search { get; set; }
        public string MembershipType { get; set; }
    }
}
