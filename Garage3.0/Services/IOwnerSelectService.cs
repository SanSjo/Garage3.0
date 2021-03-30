using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Garage3.Services
{
    public interface IOwnerSelectService
    {
        Task<IEnumerable<SelectListItem>> OwnerSelect();
    }
}