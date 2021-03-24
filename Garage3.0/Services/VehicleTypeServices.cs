using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using GarageMVC.Data;

using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GarageMVC.Services
{
    public class VehicleTypeService : IVehicleTypeService
    {
        private readonly GarageMVCContext db;

        public VehicleTypeService(GarageMVCContext context)
        {
            db = context;
        }

        public async Task<IEnumerable<SelectListItem>> GetVehicleTypes()
        {
            return await db.VehicleType.OrderBy(v => v.Name).Select(r => new SelectListItem
            {
                Text = r.Name.ToString(),
                Value = r.Name.ToString()
            }).ToListAsync();
        }
    }
}