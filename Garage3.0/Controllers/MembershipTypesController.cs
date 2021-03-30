using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Garage3.Data;
using Garage3.Models;

namespace Garage3.Controllers
{
    public class MembershipTypesController : Controller
    {
        private readonly Garage3Context db;

        public MembershipTypesController(Garage3Context context)
        {
            db = context;
        }

        // GET: MembershipTypes
        public async Task<IActionResult> Index()
        {
            return View(await db.MembershipType.ToListAsync());
        }

        // GET: MembershipTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var membershipType = await db.MembershipType
                .FirstOrDefaultAsync(m => m.MembershiptTypeID == id);
            if (membershipType == null)
            {
                return NotFound();
            }

            return View(membershipType);
        }

        // GET: MembershipTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MembershipTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MembershiptTypeID,Type,Discount")] MembershipType membershipType)
        {
            if (ModelState.IsValid)
            {
                db.Add(membershipType);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(membershipType);
        }

        // GET: MembershipTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var membershipType = await db.MembershipType.FindAsync(id);
            if (membershipType == null)
            {
                return NotFound();
            }
            return View(membershipType);
        }

        // POST: MembershipTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MembershiptTypeID,Type,Discount")] MembershipType membershipType)
        {
            if (id != membershipType.MembershiptTypeID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(membershipType);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MembershipTypeExists(membershipType.MembershiptTypeID))
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
            return View(membershipType);
        }

        // GET: MembershipTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var membershipType = await db.MembershipType
                .FirstOrDefaultAsync(m => m.MembershiptTypeID == id);
            if (membershipType == null)
            {
                return NotFound();
            }

            return View(membershipType);
        }

        // POST: MembershipTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var membersWithSelectedMembershipType = db.Member.Include(m => m.MembershipType).Where(m => m.MembershipType.MembershiptTypeID == id);
            if (membersWithSelectedMembershipType.Count()>0)
            {
                return View("MembersWithMembershipTypeExists");
            }
            var membershipType = await db.MembershipType.FindAsync(id);
            db.MembershipType.Remove(membershipType);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MembershipTypeExists(int id)
        {
            return db.MembershipType.Any(e => e.MembershiptTypeID == id);
        }
    }
}
