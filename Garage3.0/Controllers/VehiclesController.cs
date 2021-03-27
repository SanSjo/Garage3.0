using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Garage3.Data;
using Garage3.Models.ViewModels;

namespace Garage3.Models
{
    public class VehiclesController : Controller
    {
        private readonly Garage3Context db;

        public VehiclesController(Garage3Context context)
        {
            db = context;
        }

        // GET: Vehicles
        public async Task<IActionResult> Index()
        {
            List<VehicleOverviewViewModel> list = new List<VehicleOverviewViewModel>();


            foreach(Vehicle v in db.Vehicle.Include(v=>v.Owner).Include(v=>v.Owner.MembershipType).Include(v=>v.VehicleType).ToList())
            {
                list.Add(new VehicleOverviewViewModel()
                {                    
                    VehicleID = v.Id,
                    Owner = $"{v.Owner.FirstName} {v.Owner.LastName}",
                    MembershipType = v.Owner.MembershipType.Type,
                    VehicleType = v.VehicleType.Type,
                    LicenseNumber = v.LicenseNumber,
                    TimeParked =  DateTime.Now-v.ArrivalTime
                }) ;
            }

            return View(list);
        }

        // GET: Vehicles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicle = await db.Vehicle
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }

        // GET: Vehicles/RegisterNewVehicle
        public IActionResult RegisterNewVehicle()
        {
            return View();
        }

        // POST: Vehicles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterNewVehicle([Bind("Id,ArrivalTime,LicenseNumber,Color,Brand,Model,NumberOfWheels,Size")] Vehicle vehicle, Member owner)
        {
            vehicle.ArrivalTime = DateTime.Now;
            // TODO: How do we post the owner
            vehicle.Owner = owner;
            if (ModelState.IsValid)
            {
                db.Add(vehicle);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vehicle);
        }

        // GET: Vehicles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicle = await db.Vehicle.FindAsync(id);
            if (vehicle == null)
            {
                return NotFound();
            }
            return View(vehicle);
        }

        // POST: Vehicles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ArrivalTime,LicenseNumber,Color,Brand,Model,NumberOfWheels,Size")] Vehicle vehicle)
        {
            if (id != vehicle.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(vehicle);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VehicleExists(vehicle.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(vehicle);
        }

        // GET: Vehicles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicle = await db.Vehicle
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }

        // POST: Vehicles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vehicle = await db.Vehicle.FindAsync(id);
            db.Vehicle.Remove(vehicle);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VehicleExists(int id)
        {
            return db.Vehicle.Any(e => e.Id == id);
        }

        public IActionResult Parking()
        {
            return View();
        }
        public IActionResult ParkingProcess(string licenseNumber)
        {
            if (VehicleInDatabase(licenseNumber))
            {
                ParkVehicle(licenseNumber);
                // TODO: Receipt
                return View(nameof(Index));
            }
            else
            {
                return NewCarOrNewMember();
            } 
        }

        private int GetMemberID()
        {
            throw new NotImplementedException();
        }

        private IActionResult NewCarOrNewMember()
        {
            return View(nameof(NewCarOrNewMember));            
        }

        private int IdentifyMember()
        {
            throw new NotImplementedException();
            int memberID;
            return memberID;
        }

        private void ParkVehicle(string licenseNumber)
        {
            throw new NotImplementedException();
        }

        private int RegisterNewMember()
        {
            throw new NotImplementedException();
            int memberID;
            return memberID;
        }

        private bool askIfUserWantsToBecomeAMember()
        {
            throw new NotImplementedException();
        }

        private void RegisterVehicleToMember(string licenseNumber, int memberID)
        {
            throw new NotImplementedException();
        }

        private bool askIfVehicleIsToBeRegisteredToMember()
        {
            throw new NotImplementedException();
        }

        private bool askUserIfTheyAreAMember()
        {
            throw new NotImplementedException();
        }

        private bool VehicleInDatabase(string licenseNumber)
        {            
            var checkVehicle = db.Vehicle.Where(v => v.LicenseNumber == licenseNumber);
            if (checkVehicle.Count()>1)
            {
                return true;
            }
            return false;
        }
    }
}
