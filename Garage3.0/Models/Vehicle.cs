using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.AspNetCore.Mvc;

namespace Garage3.Models
{
    public class Vehicle
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public Member Owner { get; set; }

        [Display(Name = "Parking space")]
        public IList<ParkingSpace> ParkedAt { get; set; }

        [Display(Name = "Arrival time")]        
        public DateTime? ArrivalTime { get; set; }

        [Required]
        [Display(Name = "License Number")]        
        // TODO: Implement
        //[Remote(action: "VehicleIsNotParked", controller: "Vehicles", ErrorMessage = "This Vehicle is Already Parked")]
        public string LicenseNumber { get; set; }

        [Required]
        [MaxLength(20, ErrorMessage = "20 characters max")]
        public string Color { get; set; }

        [MaxLength(20, ErrorMessage = "20 characters max")]
        [Required]
        public string Brand { get; set; }

        [MaxLength(20, ErrorMessage = "20 characters max")]
        [Required]
        public string Model { get; set; }

        [Display(Name = "Number of wheels")]
        [Range(0, int.MaxValue, ErrorMessage = "Please input a non-negative value")]
        [Required]
        public int? NumberOfWheels { get; set; }

        [Display(Name = "Vehicle type")]
        public VehicleType VehicleType { get; set; }
    }
}