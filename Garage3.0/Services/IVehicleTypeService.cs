using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc.Rendering;

namespace Garage3.Services
{
    public interface IVehicleTypeService
    {
        Task<IEnumerable<SelectListItem>> GetVehicleTypes();
    }
}