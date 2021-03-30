﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Garage3.Data;
using Garage3.Models.ViewModels;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

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
        public async Task<IActionResult> Index(string search)
        {
            List<VehicleOverviewViewModel> output = await db.Vehicle
                .Join(db.Member, vehicle => vehicle.Owner, member => member, 
                (vehicle, member) => new VehicleOverviewViewModel
                {
                    VehicleID = vehicle.Id,
                    Owner = $"{member.FirstName} {member.LastName}",
                    MembershipType = member.MembershipType.Type,
                    VehicleType = vehicle.VehicleType.Type,
                    LicenseNumber = vehicle.LicenseNumber,
                    TimeParked = vehicle.ArrivalTime != null ? DateTime.Now - vehicle.ArrivalTime : null
                })
                .Where(v => String.IsNullOrEmpty(search) || (v.VehicleType.Contains(search) || v.LicenseNumber.Contains(search))
                && v.TimeParked != null)
                .ToListAsync();

            return View(output);
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

        // POST: Vehicles/Create To protect from overposting attacks, enable the specific properties
        // you want to bind to. For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterNewVehicle([Bind("MemberID,LicenseNumber,VehicleType,Color,Brand,Model,NumberOfWheels")] RegisterNewVehicleViewModel vh)
        {
            var vehicle = new Vehicle();            
            vehicle.LicenseNumber  =  vh.LicenseNumber;
            vehicle.Brand = vh.Brand;
            vehicle.Color  =  vh.Color;
            vehicle.Model = vh.Model;
            vehicle.NumberOfWheels  =  vh.NumberOfWheels;
            var owner = db.Member.Where(m => m.MemberID == Int32.Parse(vh.MemberID)).FirstOrDefault();
            var vehicleType = db.VehicleType.Where(v => v.Type == vh.VehicleType).FirstOrDefault();
            vehicle.Owner  =  owner;
            vehicle.VehicleType = vehicleType;
            
            

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


        // BUG: Cant enter decimals as discount
        // BUG: Deleteing membershiptype while member exists with that type causes crash
        // POST: Vehicles/Edit/5 To protect from overposting attacks, enable the specific properties
        // you want to bind to. For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Vehicle vehicle)
        {
            if (id != vehicle.Id)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                try
                {
                    var currentVehicle = db.Vehicle.FirstOrDefault(v => v.Id == vehicle.Id);
                    if (currentVehicle == null)
                        return NotFound();
            
                    currentVehicle.LicenseNumber = vehicle.LicenseNumber;
                    currentVehicle.Color = vehicle.Color;
                    currentVehicle.Brand = vehicle.Brand;
                    currentVehicle.Model = vehicle.Model;
                    currentVehicle.NumberOfWheels = vehicle.NumberOfWheels;

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


            return RedirectToAction(nameof(Index));
        }

        //GET: Vehicles/Delete/5
        public async Task<IActionResult> UnregisterVehicle(int? id)
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

        //public async Task<IActionResult> RetrieveVehicle(string selectVehicle)
        //{
        //    IQueryable<string> genreQuery = from m in db.Vehicle
        //                                    orderby m.LicenseNumber
        //                                    select m.LicenseNumber;

        //    var vehicle = from v in db.Vehicle
        //                 select v;

        //    if (!String.IsNullOrEmpty(selectVehicle))
        //    {
        //        vehicle = vehicle.Where(g => g.LicenseNumber == selectVehicle);
        //    }

        //    var selectVehicleVM = new RetrieveVehicleViewModel
        //    {
        //        Vehicles = new SelectList(await genreQuery.Distinct().ToListAsync()),


        //    };


        //    return View(selectVehicleVM);
        //}



        public ActionResult RetrieveParkedVehicle()
        {
            return View();
        }


        // POST: Vehicles/Delete/5
        [HttpPost, ActionName("UnregisterVehicle")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string SelectedVehicle)
        {

            if (SelectedVehicle == null)
            {
                return View();
            }

            // Check which vehicle to remove
            var vehicleToRemove = await db.Vehicle.Where(v => v.LicenseNumber == SelectedVehicle).FirstAsync();

            // Check where vehicle is parked
            var parkingSpotToFreeUp = await db.ParkingSpace.Where(v => v.Vehicle == vehicleToRemove).ToListAsync();
            foreach (var parkingSpot in parkingSpotToFreeUp)
            {
                parkingSpot.Vehicle = null;
            }

            // Remove vehicle from database
            db.Vehicle.Remove(vehicleToRemove);

            // Save changes to database
            await db.SaveChangesAsync();
        

            return RedirectToAction("Index", "Home");

        }

        private bool VehicleExists(int id)
        {
            return db.Vehicle.Any(e => e.Id == id);
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
        public IActionResult Parking()
        {
            return View();
        }

        public IActionResult ParkingProcess(string LicenseNumber)
        {
            if (VehicleInDatabase(LicenseNumber))
            {
                var returnedView = ParkVehicle(LicenseNumber);
                // TODO: Receipt
                return RedirectToAction(returnedView);
            }
            else
            {
                return NewCarOrNewMember();
            }
        }
        private string ParkVehicle(string licenseNumber)
        {
            var vehicle = db.Vehicle.Include(v => v.VehicleType).Include(v => v.ParkedAt).Where(v => v.LicenseNumber == licenseNumber).FirstOrDefault();
            // if vehicle is already parked inform user
            if (vehicle.ParkedAt.Count()>0)
            {
                return "VehicleAlreadyParked";
            }
            // if no space for vehicle
            var spaceCheck = new Garage3.Utilites.ParkingSpaceCalculations(db);            
            if (spaceCheck.vehicleTypeStatistics().Where(v => v.Type == vehicle.VehicleType.Type).Select(v => v.AmountAbleToPark).First()<=0)
            {                
                return "NoSpaceForVehicle";
            }
            var parkingSpaces = db.ParkingSpace.Include(v => v.Vehicle);
            // Get vehicle size
            var vehicleSize = vehicle.VehicleType.Size;

            if (vehicleSize < 1)
            {
                // Check each parkingspace
                foreach (var parkingSpace in parkingSpaces)
                {
                    // calculate space left
                    float totalVehicleSize = 0;
                    foreach (var vehicleInParkingSpace in parkingSpace.Vehicle)
                    {
                        totalVehicleSize += vehicleInParkingSpace.VehicleType.Size;
                    }

                    // if vehicle fits park
                    if ((parkingSpace.Size - totalVehicleSize) > vehicleSize)
                    {
                        vehicle.ParkedAt.Add(parkingSpace);
                        parkingSpace.Vehicle.Add(vehicle);
                        break;
                    }
                }
            }
            if (vehicleSize>=1)
            {                
                List<ParkingSpace> emptySpaces = new List<ParkingSpace>();                             
                foreach (var parkingspace in parkingSpaces)
                {                    
                    // find empty space
                    if (parkingspace.Vehicle.Count() == 0)
                    {                        
                        emptySpaces.Add(parkingspace);
                        // if empty spaces >= vehicle size park vehicle
                        if (emptySpaces.Count>=vehicleSize)
                        {
                            foreach (var emptySpace in emptySpaces)
                            {
                                vehicle.ParkedAt.Add(emptySpace);
                                emptySpace.Vehicle.Add(vehicle);

                            }
                            break;
                        }
                    }                    
                    else
                    {
                        // reset emptySpots
                        emptySpaces.Clear();
                    }
                }                
            }
            vehicle.ArrivalTime = DateTime.Now;
            db.SaveChanges();
            return nameof(Garage3.Controllers.HomeController.Index);
        }
        public IActionResult VehicleAlreadyParked()
        {
            return View(nameof(VehicleAlreadyParked));
        }

        public IActionResult NoSpaceForVehicle()
        {
            return View(nameof(NoSpaceForVehicle));
        }

        [HttpPost, ActionName("RetrieveParkedVehicle")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RetrieveConfirmed(string selectedVehicle)
        {
            if (String.IsNullOrEmpty(selectedVehicle))
            {
                return View();
            }

            var vehicle = await db.Vehicle.Where(v => v.LicenseNumber == selectedVehicle).FirstAsync();
            var member = await db.Member.Where(m => m == vehicle.Owner).FirstAsync();
            var parkingSpots = await db.ParkingSpace.Where(p => vehicle.ParkedAt.Contains(p)).FirstAsync();

            foreach (var parkingSpot in vehicle.ParkedAt)
            {
                parkingSpot.Vehicle.Clear();
            }

            ReceiptOverviewModel receipt = new ReceiptOverviewModel()
            {
                Member = $"{member.FirstName} {member.LastName}",
                Vehicle = vehicle.LicenseNumber,
                TimeParked = (DateTime.Now - vehicle.ArrivalTime).ToString(),
                Cost = 0,
                Savings = 0
            };

            TempData["Member"] = receipt.Member;
            TempData["Vehicle"] = receipt.Vehicle;
            TempData["Time Parked"] = receipt.TimeParked;
            TempData["Cost"] = receipt.Cost;
            TempData["Savings"] = receipt.Savings;

            vehicle.ArrivalTime = null;

            await db.SaveChangesAsync();

            return RedirectToAction("Index", "Home");
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
                if (checkVehicle.Count() > 0)
                {
                    return true;
                }
                return false;
            }
        }
    }