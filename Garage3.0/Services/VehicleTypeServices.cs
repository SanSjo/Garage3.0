using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Garage3.Data;

using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Garage3.Services
{
        public class VehicleTypeService : IVehicleTypeService
        {
            private readonly Garage3Context db;

            public VehicleTypeService(Garage3Context context)
            {
                db = context;
            }

            public async Task<IEnumerable<SelectListItem>> GetVehicleTypes()
            {
                return await db.VehicleType.OrderBy(v => v.VehicleType).Select(r => new SelectListItem
                {
                    Text = r.VehicleType.ToString(),
                    Value = r.VehicleType.ToString()
                }).ToListAsync();
            }
        }    
}