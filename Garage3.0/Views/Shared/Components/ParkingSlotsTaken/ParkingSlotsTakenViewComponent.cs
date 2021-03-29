using System;
using System.Collections.Generic;
using System.Linq;

using Garage3.Data;
using Garage3.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Garage3.Views.Shared.Components
{
    public class VehicleTypeCount
    {
        public string Type { get; set; }

        public int AmountParked { get; set; }

        public int AmountAbleToPark { get; set; }
    }

    public class ParkingSlotCount
    {
        public int SlotsTaken { get; set; }

        public int SlotsTotal { get; set; }        

        public List<VehicleTypeCount> VehicleTypeCount { get; set; }

        public ParkingSlotCount(int slotsTaken, int slotsTotal, List<VehicleTypeCount> vehicleTypeCount)
        {
            SlotsTaken = slotsTaken;
            SlotsTotal = slotsTotal;            
            VehicleTypeCount = vehicleTypeCount;
        }
    }

    [ViewComponent(Name = "ParkingSlotsTaken")]
    public class ParkingSlotsTakenViewComponent : ViewComponent
    {
        private readonly Garage3Context db;

        public ParkingSlotsTakenViewComponent(Garage3Context context)
        {
            db = context;
        }

        public IViewComponentResult Invoke()
        {
            var slotsTaken = db.ParkingSpace.Include(x => x.Vehicle).Where(y => y.Vehicle.Count() > 0).Count();

            var slotsTotal = db.ParkingSpace.Count();

            var vehicleTypeCount = getVehicleTypeCount();

            var pSlots = new ParkingSlotCount(slotsTaken, slotsTotal, vehicleTypeCount);
            return View(pSlots);
        }

        public List<VehicleTypeCount> getVehicleTypeCount()
        {
            var vehicleTypes = getVehicleTypes();
            List<VehicleTypeCount> result = new List<VehicleTypeCount>();
            foreach (var vehicleType in vehicleTypes)
            {
                var VTCO = new VehicleTypeCount();

                VTCO.Type = vehicleType.Type;

                VTCO.AmountParked = (from v in db.Vehicle.Include(y => y.VehicleType).Include(x => x.ParkedAt)
                                     where v.VehicleType.Type == vehicleType.Type && v.ParkedAt.Count() > 0
                                     select v.Id).Distinct().Count();

                // Calculates how many vehicles of a specific size can fit in this particular
                // parking space
                // if smaller or equal to 1 we dont need to worry about consequtive spaces
                if (vehicleType.Size <= 1)
                {
                    foreach (var parkingSpace in db.ParkingSpace.Include(v => v.Vehicle))
                    {
                        float totalVehicleSize = 0;
                        foreach (var vehicle in parkingSpace.Vehicle)
                        {
                            totalVehicleSize += vehicle.VehicleType.Size;
                        }
                        VTCO.AmountAbleToPark += (int)+Math.Floor((parkingSpace.Size - totalVehicleSize) / vehicleType.Size);
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
                        if (parkingspace.Vehicle.Count() > 0 || iterations>=parkingspaces.Count())                                              
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

        private List<VehicleType> getVehicleTypes()
        {
            return db.VehicleType.ToList();
        }
    }
}