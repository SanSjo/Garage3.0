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
    public class AdministrationController : Controller
    {
        private readonly Garage3Context db;

        public AdministrationController(Garage3Context context)
        {
            db = context;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
