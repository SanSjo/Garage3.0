﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Garage3.Data;
using Garage3.Models;
using Garage3.Models.ViewModels;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Garage3.Controllers
{
    public class MembersController : Controller
    {
        private readonly Garage3Context db;

        public MembersController(Garage3Context context)
        {
            db = context;
        }

        // GET: Members
        public async Task<IActionResult> Index(string search)
        {        

        List<MembersViewModel> members = await db.Member
                .Select(member => new MembersViewModel
                {
                    PersonalIdentityNumber=member.PersonalIdentityNumber,
                    Joined=member.Joined,
                    ExtendenMemberShipEndDate=member.ExtendedMemberShipEndDate,
                    MemberID = member.MemberID,
                    FirstName = member.FirstName,
                    LastName = member.LastName,
                    NrOfVehicles = (from vehicle in db.Vehicle.Where(v => v.Owner.Equals(member)) select vehicle).ToList().Count,
                    MembershipType = db.Member.Include(m => m.MembershipType).Where(m => m.MemberID == member.MemberID).Select(m => m.MembershipType.Type).FirstOrDefault()
                }).ToListAsync();

            if (!string.IsNullOrEmpty(search))
            {
                var searchParams = search.Split();

                foreach (string s in searchParams)
                {
                    members = members.Where(m => m.FirstName.Contains(s, StringComparison.OrdinalIgnoreCase) || m.LastName.Contains(s, StringComparison.OrdinalIgnoreCase)).ToList();
                }
            }

            members = members.OrderBy(m => m.FirstName.Substring(0, 2), StringComparer.Ordinal).ToList();
            
            return View(members);
        }

        // GET: Members/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var member = await db.Member
                .FirstOrDefaultAsync(m => m.MemberID == id);
            if (member == null)
            {
                return NotFound();
            }

            ViewData["OwnedVehicles"] = await (from vehicle in db.Vehicle.Where(v => v.Owner == member) select vehicle).ToListAsync();

            var memberViewModel = new MembersViewModel
               {
                   PersonalIdentityNumber = member.PersonalIdentityNumber,
                   Joined = member.Joined,
                   ExtendenMemberShipEndDate = member.ExtendedMemberShipEndDate,
                   MemberID = member.MemberID,
                   FirstName = member.FirstName,
                   LastName = member.LastName,
                   NrOfVehicles = (from vehicle in db.Vehicle.Where(v => v.Owner.Equals(member)) select vehicle).ToList().Count,
                   MembershipType = db.Member.Include(m => m.MembershipType).Where(m => m.MemberID == member.MemberID).Select(m => m.MembershipType.Type).FirstOrDefault()
               };

            return View(memberViewModel);
        }

        // GET: Members/Create
        public IActionResult RegisterNewMember()
        {
            return View();
        }

        [AcceptVerbs("GET", "POST")]
        public IActionResult SameName(string FirstName, string LastName)
        {
            if ((!string.IsNullOrEmpty(FirstName) ||
                !string.IsNullOrEmpty(LastName)) && string.Equals(FirstName,LastName, StringComparison.InvariantCultureIgnoreCase))
            {
                return Json("First and last names can't be the same");
            }

            return Json(true);
        }

        [AcceptVerbs("GET", "POST")]
        public async Task<IActionResult> IsAlreadyAMember(string PersonalIdentityNumber, int MemberID)
        {
            if (PersonalIdentityNumber == "1")
            {
                return Json("näru");
            }
            //Character checker
            if (Regex.IsMatch(PersonalIdentityNumber, @"[^0-9-]"))
            {
                return Json("Refrain from using anything other than numbers");
            }

            //format input to 12 numbered PINumber
            string PINformat = Regex.Replace(PersonalIdentityNumber, @"[^0-9]", "");

            if (PINformat.Length > 12)
            {
                return Json("Too many numbers.");
            }
            else if (PINformat.Length < 12)
            {
                return Json("To few numbers.");
            }

            //Validate age based on input
            string PINDate = PINformat.Substring(0, 8);

            try
            {
                DateTime customerAge = DateTime.ParseExact(PINDate, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
                if (customerAge > DateTime.Now.AddYears(-18))
                {
                    return Json("Too Young");
                }
            }
            catch (Exception)
            {
                return Json("Invalid date in this Personal Identity Number");
            }

            // check member id too so that edit works with remote validation
            if (await db.Member.FirstOrDefaultAsync(m => m.PersonalIdentityNumber == PersonalIdentityNumber && m.MemberID != MemberID) != null)
            {
                return Json("Member already exists");
            }

            //return Json(PINformat);
            return Json(true);

        }

        // POST: Members/Create To protect from overposting attacks, enable the specific properties
        // you want to bind to. For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterNewMember([Bind("MemberID,PersonalIdentityNumber,FirstName,LastName,Joined,ExtendedMemberShipEndDate")] Member member)
        {
            if (ModelState.IsValid)
            {

                member.Joined = DateTime.Now;

                // todo: refactor to method
                string PINformat = Regex.Replace(member.PersonalIdentityNumber, @"[^0-9]", "");
                string PINDate = PINformat.Substring(0, 8);
                DateTime customerAge = DateTime.ParseExact(PINDate, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);

                // give automatic pro membership at join
                if (customerAge < DateTime.Now.AddYears(-65))
                {
                    member.ExtendedMemberShipEndDate = DateTime.Now.AddYears(2);
                }
                else
                {
                    member.ExtendedMemberShipEndDate = DateTime.Now.AddDays(30);
                }
                member.MembershipType = db.MembershipType.Where(m => m.Type == "Pro").FirstOrDefault();
                db.Add(member);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(member);
        }

        // GET: Members/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var member = await db.Member.FindAsync(id);
            if (member == null)
            {
                return NotFound();
            }
            return View(member);
        }

        // POST: Members/Edit/5 To protect from overposting attacks, enable the specific properties
        // you want to bind to. For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MemberID,PersonalIdentityNumber,FirstName,LastName,Joined,ExtendedMemberShipEndDate")] Member member)
        {
            if (id != member.MemberID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(member);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MemberExists(member.MemberID))
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
            return View(member);
        }

        // GET: Members/Delete/5
        public async Task<IActionResult> UnregisterMember(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var member = await db.Member
                .FirstOrDefaultAsync(m => m.MemberID == id);
            if (member == null)
            {
                return NotFound();
            }

            return View(member);
        }

        // POST: Members/Delete/5
        [HttpPost, ActionName("UnregisterMember")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var member = await db.Member.FindAsync(id);            
            var memberVehicles = db.Vehicle.Where(v => v.Owner == member);

            // removes all vehicles registered to member
            foreach (var vehicle in memberVehicles)
            {
                db.Vehicle.Remove(vehicle);
            }
            db.Member.Remove(member);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MemberExists(int id)
        {
            return db.Member.Any(e => e.MemberID == id);
        }
    }
}