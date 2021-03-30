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
        [Display(Name = "Personal Identity Number")]
        [Remote(action: nameof(MembersController.IsAlreadyAMember), controller: "Members" ,AdditionalFields="MemberID")]        
        public string PersonalIdentityNumber { get; set; }

        // TODO: Add check that first name isnt the same as last name
        [Display(Name = "First Name")]
        [MinLength(2, ErrorMessage = "Name must be at least 2 characters long.")]
        [Remote(action: nameof(MembersController.SameName), controller: "Members", AdditionalFields = "LastName")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [MinLength(2, ErrorMessage = "Name must be at least 2 characters long.")]
        [Remote(action: nameof(MembersController.SameName), controller: "Members", AdditionalFields = "FirstName")]
        public string LastName { get; set; }

        public MembershipType MembershipType { get; set; }

        public DateTime Joined { get; set; }

        [Display(Name = "Membership End Date")]
        [DataType(DataType.DateTime)]
        public DateTime? ExtendedMemberShipEndDate { get; set; }
    }
}