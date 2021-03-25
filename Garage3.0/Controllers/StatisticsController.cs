using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Garage3.Data;

using Microsoft.AspNetCore.Mvc;

namespace Garage3.Controllers
{
    public class StatisticsController : Controller
    {        
            private readonly Garage3Context db;

            public StatisticsController(Garage3Context context)
            {
                db = context;
            }
            public IActionResult Index()
        {
            return View();
        }
    }
}
