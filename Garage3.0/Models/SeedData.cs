using System;
using System.Linq;

using Garage3.Data;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

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
                    goto vehicleTypeGen;
                }
                context.MembershipType.AddRange(
                    new MembershipType { Type = "Pro", Discount = 10 },
                    new MembershipType { Type = "Standard", Discount = 0 }
                  );

            vehicleTypeGen:

                if (context.VehicleType.Any())
                {
                    goto parkingSpaceGen;
                }

                context.VehicleType.AddRange(
                    new VehicleType { Type = "Car", Size = 1 },
                    new VehicleType { Type = "Motorcycle", Size = 1/3 },
                    new VehicleType { Type = "Pickup", Size = 2 },
                    new VehicleType { Type = "Truck", Size = 3 }
                  );

            parkingSpaceGen:

                if (context.ParkingSpace.Any())
                {
                    goto savechanges;
                }

                for (int i = 1; i < 10; i++)
                {
                    context.ParkingSpace.AddRange(
                    new ParkingSpace { Number = i, Name = "Ruta" + i, Size=1 });
                }

            savechanges:
                context.SaveChanges();
            }
        }

        public static void GenerateMemberData(IServiceProvider serviceProvider)
        {
            using (var context = new Garage3Context(serviceProvider.GetRequiredService<DbContextOptions<Garage3Context>>()))
            {
                if (!context.Member.Any())
                {
                    var standard = context.MembershipType.FirstOrDefault(x => x.Type == "Standard");
                    var pro = context.MembershipType.FirstOrDefault(x => x.Type == "Pro");

                    context.Member.AddRange(
                            new Member { PersonalIdentityNumber = "198002309876", FirstName = "Conny", LastName = "Andersson", Joined = DateTime.Parse("2020-03-01"), ExtendedMemberShipEndDate = DateTime.Parse("2021-02-01"), MembershipType = standard },
                            new Member { PersonalIdentityNumber = "198102309876", FirstName = "Berta", LastName = "Svennson", Joined = DateTime.Parse("2021-01-01"), ExtendedMemberShipEndDate = DateTime.Parse("2021-05-06"), MembershipType = pro },
                            new Member { PersonalIdentityNumber = "198202309876", FirstName = "David", LastName = "Nokto", Joined = DateTime.Parse("2018-11-01"), ExtendedMemberShipEndDate = DateTime.Parse("2021-03-12"), MembershipType = standard },
                            new Member { PersonalIdentityNumber = "198302309876", FirstName = "Anita", LastName = "Berg", Joined = DateTime.Parse("2019-06-01"), ExtendedMemberShipEndDate = DateTime.Parse("2021-03-24"), MembershipType = standard },
                            new Member { PersonalIdentityNumber = "193002309876", FirstName = "Stefan", LastName = "Karlsson", Joined = DateTime.Parse("2021-03-01"), ExtendedMemberShipEndDate = DateTime.Parse("2021-04-01"), MembershipType = pro }
                        );
                    context.SaveChanges();
                }
            }
        }

        public static void GenerateVehicleData(IServiceProvider serviceProvider)
        {
            using (var context = new Garage3Context(serviceProvider.GetRequiredService<DbContextOptions<Garage3Context>>()))
            {
                if (!context.Vehicle.Any())
                {
                    var car = context.VehicleType.FirstOrDefault(x => x.Type == "Car");
                    var motorcycle = context.VehicleType.FirstOrDefault(x => x.Type == "Motorcycle");
                    var pickup = context.VehicleType.FirstOrDefault(x => x.Type == "Pickup");
                    var truck = context.VehicleType.FirstOrDefault(x => x.Type == "Truck");

                    var owner1 = context.Member.Where(m => m.MemberID == 1).FirstOrDefault();
                    var owner2 = context.Member.Where(m => m.MemberID == 2).FirstOrDefault();
                    var owner3 = context.Member.Where(m => m.MemberID == 3).FirstOrDefault();
                    var owner4 = context.Member.Where(m => m.MemberID == 4).FirstOrDefault();
                    var owner5 = context.Member.Where(m => m.MemberID == 5).FirstOrDefault();

                    context.Vehicle.AddRange(
                        new Vehicle { Owner = owner1, ArrivalTime = DateTime.Parse("2021-03-02"), Brand = "Volvo", NumberOfWheels = 4, LicenseNumber = "ABC123", Color = "Black", Model = "E4", VehicleType = car },
                        new Vehicle { Owner = owner2, ArrivalTime = DateTime.Parse("2021-03-02"), Brand = "Yamaha", NumberOfWheels = 2, LicenseNumber = "VBA234", Color = "Brown", Model = "E5", VehicleType = motorcycle },
                        new Vehicle { Owner = owner3, ArrivalTime = DateTime.Parse("2021-03-02"), Brand = "Toyota", NumberOfWheels = 4, LicenseNumber = "DHI137", Color = "Orangie", Model = "E0", VehicleType = pickup },
                        new Vehicle { Owner = owner4, ArrivalTime = DateTime.Parse("2021-03-02"), Brand = "Mercedes", NumberOfWheels = 6, LicenseNumber = "BCA354", Color = "DowerBlue", Model = "E9", VehicleType = truck },
                        new Vehicle { Owner = owner5, ArrivalTime = DateTime.Parse("2021-03-02"), Brand = "Harley", NumberOfWheels = 2, LicenseNumber = "DCI298", Color = "MaybeYw", Model = "EU", VehicleType = motorcycle }
                        );

                    context.SaveChanges();
                }
            }
        }
    }
}