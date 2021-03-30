using System;
using System.Collections.Generic;
using System.Linq;
using Garage3.Utilites;
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

        public List<Utilites.VehicleTypeCount> VehicleTypeCount { get; set; }        
        public ParkingSlotCount(int slotsTaken, int slotsTotal, List<Utilites.VehicleTypeCount> vehicleTypeCount)
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

            var GTVC = new ParkingSpaceCalculations(db);

            var vehicleTypeStatistics = GTVC.vehicleTypeStatistics();

            var pSlots = new ParkingSlotCount(slotsTaken, slotsTotal, vehicleTypeStatistics);
            return View(pSlots);
        }        
    }
}