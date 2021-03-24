using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Garage3._0.Models
{
    public class Member
    {
        public int IDNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public MembershipType MembershipType { get; set; }
        public DateTime Joined { get; set; }
        public DateTime? ExtendedMemberShipEndDate { get; set; }
    }
}
