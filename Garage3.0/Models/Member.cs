using Garage3.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Garage3.Models
{
    public class Member
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MemberID { get; set; }

        
        [Remote(action: nameof(MembersController.IsAlreadyAMember), controller: "Members")]        
        public string PersonalIdentityNumber { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public MembershipType MembershipType { get; set; }

        public DateTime Joined { get; set; }

        public DateTime? ExtendedMemberShipEndDate { get; set; }
    }
}