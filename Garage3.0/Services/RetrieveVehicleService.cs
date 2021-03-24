//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//using GarageMVC.Data;

//using Microsoft.AspNetCore.Mvc.Rendering;
//using Microsoft.EntityFrameworkCore;

namespace Garage3.Services
{
//namespace GarageMVC.Services
//{
//    public class RetrieveVehicleService : IRetrieveVehicleService
//    {
//        private readonly GarageMVCContext db;

//        public RetrieveVehicleService(GarageMVCContext context)
//        {
//            db = context;
//        }

//        public async Task<IEnumerable<SelectListItem>> GetParkedVehicles()
//        {
//            return await db.Vehicle.OrderBy(v => v.RegistrationNumber).Select(r => new SelectListItem
//            {
//                Text = r.RegistrationNumber.ToString(),
//                Value = r.RegistrationNumber.ToString()
//            }).ToListAsync();
//        }
//    }
//}
}