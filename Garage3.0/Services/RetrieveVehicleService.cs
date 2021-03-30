using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Garage3.Data;



using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Garage3.Services
{    
        public class RetrieveVehicleService : IRetrieveVehicleService
        {
            private readonly Garage3Context db;

            public RetrieveVehicleService(Garage3Context context)
            {
                db = context;
            }

            public async Task<IEnumerable<SelectListItem>> GetParkedVehicles()
            {
                return await db.Vehicle.OrderBy(v => v.LicenseNumber).Where(v => v.ArrivalTime != null).Select(r => new SelectListItem
                {
                    Text = r.LicenseNumber.ToString(),
                    Value = r.LicenseNumber.ToString()
                }).ToListAsync();
            }
        }    
}