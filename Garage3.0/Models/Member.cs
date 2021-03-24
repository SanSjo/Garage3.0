using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Garage3.Models
{
    public class Member
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MemberPK { get; set; }

        public string IDNumber { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public MembershipType MembershipType { get; set; }

        public DateTime Joined { get; set; }

        public DateTime? ExtendedMemberShipEndDate { get; set; }
    }
}