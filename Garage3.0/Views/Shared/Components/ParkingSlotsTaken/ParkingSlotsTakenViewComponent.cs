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

        public bool IsGarageFull { get; set; }

        public List<VehicleTypeCount> VehicleTypeCount { get; set; }
    

        public ParkingSlotCount(int slotsTaken, int slotsTotal, bool isGarageFull, List<VehicleTypeCount> vehicleTypeCount)
        {
            SlotsTaken = slotsTaken;
            SlotsTotal = slotsTotal;
            IsGarageFull = isGarageFull;
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

            var pSlots = new ParkingSlotCount(slotsTaken, slotsTotal, IsGarageFull(), vehicleTypeCount);
            return View(pSlots);
        }

        
        public List<VehicleTypeCount> getVehicleTypeCount()
        {
            var vehicleTypes = getVehicleTypes();
            List<VehicleTypeCount> result;
            foreach (var vehicleType in vehicleTypes)
            {
                var VTCO = new VehicleTypeCount();
                VTCO.Type = vehicleType.Type;
                var parkedCarsTemp = from v in db.Vehicle.Include(y => y.VehicleType).Include(x => x.ParkedAt)
                                     where v.VehicleType.Type == vehicleType.Type && v.ParkedAt != null
                                     select v.Id;

                VTCO.AmountParked = parkedCarsTemp.Distinct().Count();
               

            }
            return result;
        }

        private List<VehicleType> getVehicleTypes()
        {
            return db.VehicleType.ToList();

        }

        public bool IsGarageFull()
        {
            // WORKS
            var motorcyclesInGarage =
                from v in db.Vehicle.Include(y => y.VehicleType).Include(x => x.ParkedAt)
                 where v.VehicleType.Type == "Motorcycle"
                 select v.ParkedAt;

            var parkingSpacesTakenByMotorcycles = motorcyclesInGarage.SelectMany(v => v).ToList();

            var parkingSpaceGroups =
                from m in parkingSpacesTakenByMotorcycles
                group m by m into groups
                select groups.Count();

            foreach (var parkingSpace in parkingSpaceGroups)
            {
                if (parkingSpace < 3)
                {
                    return false;
                }
            }

            return true;
        }
    }
}