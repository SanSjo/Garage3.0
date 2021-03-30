using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Garage3.Data;
using Garage3.Models;

using Microsoft.EntityFrameworkCore;

namespace Garage3.Utilites
{
    public class VehicleTypeCount
    {
        public string Type { get; set; }

        public int AmountParked { get; set; }

        public int AmountAbleToPark { get; set; }

        public string ImgSrc { get; set; }
    }

    public class ParkingSpaceCalculations : IParkingSpaceCalculations
    {
        private Garage3Context db;        

        public ParkingSpaceCalculations(Garage3Context context)
        {
            db = context;
        }
        public List<VehicleTypeCount> vehicleTypeStatistics()
        {
            var vehicleTypes = db.VehicleType.ToList();
            List<VehicleTypeCount> result = new List<VehicleTypeCount>();

            foreach (var vehicleType in vehicleTypes)
            {
                var VTCO = new VehicleTypeCount();

                VTCO.ImgSrc = vehicleType.imgSrc;

                VTCO.Type = vehicleType.Type;

                VTCO.AmountParked = (from v in db.Vehicle.Include(y => y.VehicleType).Include(x => x.ParkedAt)
                                     where v.VehicleType.Type == vehicleType.Type && v.ParkedAt.Count() > 0
                                     select v.Id).Distinct().Count();

                // Calculates how many vehicles of a specific size can fit in this particular
                // parking space
                // if smaller or equal to 1 we dont need to worry about consequtive spaces
                if (vehicleType.Size <= 1)
                {
                    var parkingSpaces = db.ParkingSpace.Include(v => v.Vehicle);
                    foreach (var parkingSpace in parkingSpaces)
                    {
                        float totalVehicleSize = 0;
                        foreach (var vehicle in parkingSpace.Vehicle)
                        {
                            totalVehicleSize += vehicle.VehicleType.Size;
                        }
                        VTCO.AmountAbleToPark += (int)+Math.Floor((parkingSpace.Size - Math.Min(1,totalVehicleSize)) / vehicleType.Size);
                    }
                }
                if (vehicleType.Size > 1)
                {
                    int vehicleParkingspaces = 0;
                    int emptySpots = 0;
                    var parkingspaces = db.ParkingSpace.Include(v => v.Vehicle);
                    var iterations = 0;

                    foreach (var parkingspace in parkingspaces)
                    {
                        iterations++;
                        // find first empty space
                        if (parkingspace.Vehicle.Count() == 0)
                        {
                            // count empty spots
                            emptySpots++;                            
                        }
                        if (parkingspace.Vehicle.Count() > 0 || iterations >= parkingspaces.Count())
                        {
                            // divide by vehicle size round down add to count
                            vehicleParkingspaces = vehicleParkingspaces + (int)Math.Floor(emptySpots / vehicleType.Size);
                            emptySpots = 0;
                        }
                    }
                    VTCO.AmountAbleToPark = vehicleParkingspaces;
                }

                result.Add(VTCO);
            }
            return result;
        }       
    }
}
