using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace MaintenanceFilter
{
	public interface IMaintenanceSettingProvider
	{
		DateTime StartTimeUtc { get; }
		DateTime EndTimeUtc { get; }
		string MaintenanceWarningMessage { get; }
		double WarningLeadTime { get; }
		bool CanByPass(IPrincipal user);
		string GetMaintenanceUrl(HttpRequest request);
	}
}
