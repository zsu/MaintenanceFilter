using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Text;

namespace MaintenanceFilter
{
    public class MaintenanceAttribute:Attribute, IFilterMetadata
    {
        public bool Disabled { get; set; }
    }
}
