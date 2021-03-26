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
        public static void GenerateTypes(IServiceProvider serviceProvider)
        {
            using (var context = new Garage3Context(serviceProvider.GetRequiredService<DbContextOptions<Garage3Context>>()))
            {

                if (context.MembershipType.Any())
                {
                    goto vehicletypegen;
                }
                context.MembershipType.AddRange(
                    new MembershipType { Type = "Pro", Discount = 10 },
                    new MembershipType { Type = "Member", Discount = 0 }
                  );

            vehicletypegen:

                if (context.VehicleType.Any())
                {
                    goto vehiclegen;
                }

                context.VehicleType.AddRange(
                    new VehicleType { Type = "Car", Size = 1 },
                    new VehicleType { Type = "Motorcycle", Size = 1 },
                    new VehicleType { Type = "Pickup", Size = 2 },
                    new VehicleType { Type = "Truck", Size = 3 }
                  );

            vehiclegen:




                if (context.ParkingSpace.Any())
                {
                    goto savechanges;
                }

                for (int i = 1; i < 10; i++)
                {
                    context.ParkingSpace.AddRange(
                    new ParkingSpace { Number = i, Name = "Ruta" + i });
                }

            savechanges:
                context.SaveChanges();

            }

        }
        public static void GenerateGeneralData(IServiceProvider serviceProvider)
        {
            using (var context = new Garage3Context(serviceProvider.GetRequiredService<DbContextOptions<Garage3Context>>()))
            {
                if (context.Member.Any())
                {
                    goto vehiclegen;
                }

                context.Member.AddRange(

                        new Member { PersonalIdentityNumber = "198002309876", FirstName = "Conny", LastName = "Andersson", Joined = DateTime.Parse("2021-03-01"), ExtendedMemberShipEndDate = DateTime.Parse("2021-04-01") },
                        new Member { PersonalIdentityNumber = "198102309876", FirstName = "Berta", LastName = "Svennson", Joined = DateTime.Parse("2021-03-01"), ExtendedMemberShipEndDate = DateTime.Parse("2021-04-01") },
                        new Member { PersonalIdentityNumber = "198202309876", FirstName = "David", LastName = "Nokto", Joined = DateTime.Parse("2021-03-01"), ExtendedMemberShipEndDate = DateTime.Parse("2021-04-01") },
                        new Member { PersonalIdentityNumber = "198302309876", FirstName = "Anita", LastName = "Berg", Joined = DateTime.Parse("2021-03-01"), ExtendedMemberShipEndDate = DateTime.Parse("2021-04-01") },
                        new Member { PersonalIdentityNumber = "193002309876", FirstName = "Stefan", LastName = "Karlsson", Joined = DateTime.Parse("2021-03-01"), ExtendedMemberShipEndDate = DateTime.Parse("2021-04-01") }


                    );

                vehiclegen:

                if (context.Vehicle.Any())
                {
                    goto savechanges;
                }

                context.Vehicle.AddRange(
                    new Vehicle { ArrivalTime = DateTime.Parse("2021-03-02"), Brand = "Volvo", NumberOfWheels = 4, LicenseNumber = "ABC123", Color = "Black", Model = "E4", VehicleType =context.VehicleType.FirstOrDefault(x => x.Type == "Car")},
                    new Vehicle { ArrivalTime = DateTime.Parse("2021-03-02"), Brand = "Yamaha", NumberOfWheels = 2, LicenseNumber = "VBA234", Color = "Brown", Model = "E#" },
                    new Vehicle { ArrivalTime = DateTime.Parse("2021-03-02"), Brand = "Toyota", NumberOfWheels = 4, LicenseNumber = "DHI137", Color = "Orangie", Model = "E0" },
                    new Vehicle { ArrivalTime = DateTime.Parse("2021-03-02"), Brand = "Mercedes", NumberOfWheels = 6, LicenseNumber = "BCA354", Color = "DowerBlue", Model = "E9" },
                    new Vehicle { ArrivalTime = DateTime.Parse("2021-03-02"), Brand = "Harley", NumberOfWheels = 2, LicenseNumber = "DCI298", Color = "MaybeYellow?", Model = "EU" }
                    );
                savechanges:
                context.SaveChanges();
            }
        }
    }
}
