using MaintenanceFilter;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;

namespace Example
{
    public class MaintenanceSettingProvider:IMaintenanceSettingProvider
    {
        public DateTime EndTimeUtc
        {
            get
            {
                return DateTime.MaxValue;
            }
        }

        public string GetMaintenanceUrl(HttpRequest request)
        {
            string fullyQualifiedUrl = string.Format("{0}://{1}/Maintenance",request.Scheme,request.Host);
            return fullyQualifiedUrl;
        }

        public string MaintenanceWarningMessage
        {
            get
            {
                return string.Format("The site is going to be down for maintenance at {0} UTC.", StartTimeUtc);
            }
        }

        public DateTime StartTimeUtc
        {
            get
            {
                return DateTime.UtcNow.AddHours(2);
            }
        }

        public double WarningLeadTime
        {
            get
            {
                return 24 * 60 * 60;
            }
        }

        public bool CanByPass(IPrincipal user)
        {
            return false;
        }
    }
}
