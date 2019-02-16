using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MaintenanceFilter;
using Microsoft.AspNetCore.Mvc;

namespace Example.Controllers
{
    [Maintenance(Disabled =true)]
    public class SiteMaintenanceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}