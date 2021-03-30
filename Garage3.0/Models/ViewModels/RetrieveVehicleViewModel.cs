using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Garage3.Models.ViewModels
{
    public class RetrieveVehicleViewModel
    {
        public SelectList Vehicles { get; set; }
        public string SelectedVehicle { get; set; }
    }
}
