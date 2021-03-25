using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Garage3.Data;
using Garage3.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Garage3.Controllers
{
    public class MembersController : Controller
    {
        private readonly Garage3Context _context;

        public MembersController(Garage3Context context)
        {
            _context = context;
        }

        // GET: Members
        public async Task<IActionResult> Index(string search)
        {
            var members = from m in _context.Member select m;

            if (!string.IsNullOrEmpty(search))
            {
                var searchParams = search.Split();

                foreach (string s in searchParams)
                {
                    members = members.Where(m => m.FirstName.Contains(s) || m.LastName.Contains(s));
                }
            }

            List<MembersViewModel> filteredList = new List<MembersViewModel>();
            
            foreach (Member m in members)
            {
                filteredList.Add(new MembersViewModel()
                {
                    MemberID = m.MemberID,
                    FirstName = m.FirstName,
                    LastName = m.LastName,
                    NrOfVehicles = (from vehicle in _context.Vehicle.Where(v => v.Owner.Equals(m)) select vehicle)
                                                      .ToList().Count
                });
            }

            filteredList = filteredList.OrderBy(m => m.FirstName.Substring(0, 2), StringComparer.Ordinal).ToList();
            
            return View(filteredList);
        }

        // GET: Members/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var member = await _context.Member
                .FirstOrDefaultAsync(m => m.MemberID == id);
            if (member == null)
            {
                return NotFound();
            }

            return View(member);
        }

        // GET: Members/Create
        public IActionResult CreateMember()
        {
            return View();
        }

        [AcceptVerbs("GET", "POST")]
        public IActionResult IsAlreadyAMember(string PersonalIdentityNumber)
        {
            if (Regex.IsMatch(PersonalIdentityNumber, @"[^0-9-]"))
            {
                return Json("Refrain from using anything other than numbers");
            }

            string PINformat = Regex.Replace(PersonalIdentityNumber, @"[^0-9]", "");

            if (PINformat.Length > 12)
            {
                return Json("Too many numbers.");
            }
            else if (PINformat.Length < 12)
            {
                return Json("To few numbers.");
            }

            string PINDate = PINformat.Substring(0, 8);
            DateTime customerAge = DateTime.ParseExact(PINDate, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);

            if (customerAge > DateTime.Now.AddYears(-18))
            {
                return Json("Too Young");
            }

            return Json(PINformat);
        }

        // POST: Members/Create To protect from overposting attacks, enable the specific properties
        // you want to bind to. For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateMember([Bind("MemberID,PersonalIdentityNumber,FirstName,LastName,Joined,ExtendedMemberShipEndDate")] Member member)
        {
            if (ModelState.IsValid)
            {
                member.Joined = DateTime.Now;
                member.ExtendedMemberShipEndDate = DateTime.Now.AddDays(30);

                _context.Add(member);
                await _context.SaveChangesAsync();
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

            var member = await _context.Member.FindAsync(id);
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
                    _context.Update(member);
                    await _context.SaveChangesAsync();
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
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var member = await _context.Member
                .FirstOrDefaultAsync(m => m.MemberID == id);
            if (member == null)
            {
                return NotFound();
            }

            return View(member);
        }

        // POST: Members/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var member = await _context.Member.FindAsync(id);
            _context.Member.Remove(member);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MemberExists(int id)
        {
            return _context.Member.Any(e => e.MemberID == id);
        }
    }
}