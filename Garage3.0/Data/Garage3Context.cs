using Garage3.Models;

using Microsoft.EntityFrameworkCore;

namespace Garage3.Data
{
    public class Garage3Context : DbContext
    {
        public Garage3Context(DbContextOptions<Garage3Context> options)
            : base(options)
        {
        }

        public DbSet<Vehicle> Vehicle { get; set; }

        public DbSet<VehicleType> VehicleType { get; set; }

        public DbSet<Member> Member { get; set; }

        public DbSet<MembershipType> MembershipType { get; set; }

        public DbSet<ParkingSpace> ParkingSpace { get; set; }

        public DbSet<Booking> Booking { get; set; }
    }
}