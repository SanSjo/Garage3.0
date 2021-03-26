using Garage3.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Garage3.Models
{
    public class SeedData
    {
        public static void Initializer(IServiceProvider serviceProvider)
        {
            using (var context = new Garage3Context(serviceProvider.GetRequiredService<DbContextOptions<Garage3Context>>()))
            {
                if (context.Member.Any())
                {
                    return;
                }

                context.Member.AddRange(

                        new Member { PersonalIdentityNumber = "198002309876", FirstName = "Conny", LastName = "Andersson", MembershipType = { Type = "Pro", Discount = 10 }, Joined = DateTime.Parse("2021-03-01"), ExtendedMemberShipEndDate = DateTime.Parse("2021-04-01") },
                        new Member { PersonalIdentityNumber = "198102309876", FirstName = "Berta", LastName = "Svennson", MembershipType = { Type = "Pro", Discount = 10 }, Joined = DateTime.Parse("2021-03-01"), ExtendedMemberShipEndDate = DateTime.Parse("2021-04-01") },
                        new Member { PersonalIdentityNumber = "198202309876", FirstName = "David", LastName = "Nokto", MembershipType = { Type = "Pro", Discount = 10 }, Joined = DateTime.Parse("2021-03-01"), ExtendedMemberShipEndDate = DateTime.Parse("2021-04-01") },
                        new Member { PersonalIdentityNumber = "198302309876", FirstName = "Anita", LastName = "Berg", MembershipType = { Type = "Pro", Discount = 10 }, Joined = DateTime.Parse("2021-03-01"), ExtendedMemberShipEndDate = DateTime.Parse("2021-04-01") },
                        new Member { PersonalIdentityNumber = "193002309876", FirstName = "Stefan", LastName = "Karlsson", MembershipType = { Type = "Pro", Discount = 10 }, Joined = DateTime.Parse("2021-03-01"), ExtendedMemberShipEndDate = DateTime.Parse("2021-04-01") }


                    );
                context.SaveChanges();

                if (context.MembershipType.Any())
                {
                    return;
                }
                context.MembershipType.AddRange(
                     new MembershipType { Type = "Pro", Discount = 10 },
                    new MembershipType { Type = "Member", Discount = 0 }
                  );
              
                context.SaveChanges();

                if (context.Vehicle.Any())
                {
                    return;
                }

                context.Vehicle.AddRange(
                    new Vehicle { Owner = { PersonalIdentityNumber = "198002309876", FirstName = "Conny", LastName = "Andersson" }, ArrivalTime = DateTime.Parse("2021-03-02"), Brand = "Volvo", NumberOfWheels = 4, LicenseNumber = "ABC123", Color = "Black", Model = "E4", VehicleType = { Type = "Car" } },
                    new Vehicle { Owner = { PersonalIdentityNumber = "198102309876", FirstName = "Berta", LastName = "Svennson" }, ArrivalTime = DateTime.Parse("2021-03-02"), Brand = "Volvo", NumberOfWheels = 4, LicenseNumber = "ABC123", Color = "Black", Model = "E4", VehicleType = { Type = "Car" } },
                    new Vehicle { Owner = { PersonalIdentityNumber = "198202309876", FirstName = "David", LastName = "Nokto" }, ArrivalTime = DateTime.Parse("2021-03-02"), Brand = "Volvo", NumberOfWheels = 4, LicenseNumber = "ABC123", Color = "Black", Model = "E4", VehicleType = { Type = "Car" } }
                    );

                context.SaveChanges();

                if (context.VehicleType.Any())
                {
                    return;
                }

                context.VehicleType.AddRange(
                    new VehicleType { Type = "Car", Size = 1 },
                    new VehicleType { Type = "Car", Size = 1 }
                  );
           
                context.SaveChanges();

                if (context.ParkingSpace.Any())
                {
                    return;
                }

                context.ParkingSpace.AddRange(
                    new ParkingSpace { Number = 1, Name = "One" },
                    new ParkingSpace { Number = 2, Name = "Two" }
                    );

                context.SaveChanges();

                if (context.ParkingSpace.Any())
                {
                    return;
                }

                context.Booking.AddRange(
                    new Booking { BookedTime = DateTime.Parse("2021-03-20"), BookedBy = { PersonalIdentityNumber = "198002309876" } },
                    new Booking { BookedTime = DateTime.Parse("2021-03-20"), BookedBy = { PersonalIdentityNumber = "198002309876" } }
                );

                context.SaveChanges();
            }

                




        }
    }
}
