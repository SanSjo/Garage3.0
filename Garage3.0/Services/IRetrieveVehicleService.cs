using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc.Rendering;

namespace GarageMVC.Services
{
    public interface IRetrieveVehicleService
    {
        Task<IEnumerable<SelectListItem>> GetParkedVehicles();
    }
}