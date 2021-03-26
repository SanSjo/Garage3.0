using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Garage3.Models;

namespace Garage3.Data
{
    public class Garage3Context : DbContext
    {
        public Garage3Context (DbContextOptions<Garage3Context> options)
            : base(options)
        {
        }

        public DbSet<Garage3.Models.Vehicle> Vehicle { get; set; }
        //public DbSet<Garage3.Models.Vehicle> VehicleType { get; set; }
        public DbSet<Garage3.Models.VehicleType> VehicleType { get; set; }
        public DbSet<Garage3.Models.Member> Member { get; set; }
        public DbSet<MembershipType> MembershipType { get; set; }
        public DbSet<ParkingSpace> ParkingSpace { get; set; }
        public DbSet<Garage3.Models.Booking> Booking { get; set; }
    }
}
