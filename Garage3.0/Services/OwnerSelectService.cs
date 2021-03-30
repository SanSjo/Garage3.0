using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Garage3.Data;

using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Garage3.Services
{
    public class OwnerSelectService : IOwnerSelectService
    {
        private readonly Garage3Context db;

        public OwnerSelectService(Garage3Context context)
        {
            db = context;
        }

        public async Task<IEnumerable<SelectListItem>> OwnerSelect()
        {
            return await db.Member.OrderBy(v => v.FirstName).Select(r => new SelectListItem
            {
                Text = r.FirstName + " " + r.LastName,
                Value = r.MemberID.ToString()
            }).ToListAsync();

        }
    }
}