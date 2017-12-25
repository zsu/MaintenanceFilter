using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace MaintenanceFilter
{
    [Route("api/[controller]/[action]")]
    [Maintenance(Disabled =true)]
    public class MaintenanceFilterController:Controller
    {
        IHttpContextAccessor _httpContextAccessor;
        public MaintenanceFilterController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        [HttpPost]
        public void MarkShowed()
        {
            _httpContextAccessor.HttpContext.Response.Cookies.Append(MaintenanceActionFilter.KeyMaintenanceWarningCookie, "1", new CookieOptions { HttpOnly = true });
        }
    }
}
