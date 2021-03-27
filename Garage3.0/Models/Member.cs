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

        // TODO: check PID so that the month and day are valid numbers
        [Remote(action: nameof(MembersController.IsAlreadyAMember), controller: "Members")]        
        public string PersonalIdentityNumber { get; set; }

        // TODO: Add check that first name isnt the same as last name
        [MinLength(2, ErrorMessage = "Name must be at least 2 characters long.")]
        public string FirstName { get; set; }

        [MinLength(2, ErrorMessage = "Name must be at least 2 characters long.")]
        public string LastName { get; set; }

        public MembershipType MembershipType { get; set; }

        public DateTime Joined { get; set; }

        public DateTime? ExtendedMemberShipEndDate { get; set; }
    }
}