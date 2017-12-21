using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MaintenanceFilter;
using Microsoft.AspNetCore.Mvc;

namespace Example.Controllers
{
    [Maintenance(Disabled =true)]
    public class MaintenanceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}