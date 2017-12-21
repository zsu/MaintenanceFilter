﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SessionMessage.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MaintenanceFilter
{
    public class MaintenanceActionFilter : IActionFilter
    {
        private const string KeyMaintenanceWarningMessage = "maintenance.warningmessage";
        private const string KeyMaintenanceWarningCookie = "maintenancewarning";
        private ISessionMessageManager _sessionMessageManager;
        private IMaintenanceSettingProvider _settingProvider;
        public MaintenanceActionFilter(ISessionMessageManager sessionMessageManager, IMaintenanceSettingProvider settingProvider)
        {
            _sessionMessageManager = sessionMessageManager;
            _settingProvider = settingProvider;
        }
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var maintenanceAttribute = filterContext.ActionDescriptor.FilterDescriptors.Select(x => x.Filter).OfType<MaintenanceAttribute>().FirstOrDefault();
            bool disabled=false;
            if (maintenanceAttribute != null)
            {
                disabled = maintenanceAttribute.Disabled;
            }
            if (!disabled)
            {
                if (_settingProvider == null)
                    throw new Exception("IMaintenanceSettingProvider must be provided when Disabled is false.");
                var startTime = _settingProvider.StartTimeUtc;
                var endTime = _settingProvider.EndTimeUtc;
                var warningLead = _settingProvider.WarningLeadTime;
                var maintenanceWarningMessage = _settingProvider.MaintenanceWarningMessage;
                bool canBypass = _settingProvider.CanByPass(filterContext.HttpContext.User);
                var request = filterContext.HttpContext.Request;
                if (!canBypass && startTime != default(DateTime) && DateTime.UtcNow >= startTime)
                {
                    if (endTime == default(DateTime) || DateTime.UtcNow <= endTime)
                    {
                        filterContext.Result = new RedirectResult(_settingProvider.GetMaintenanceUrl(request));
                    }
                }
                else if (startTime != default(DateTime) && startTime > DateTime.UtcNow && warningLead > 0)
                {
                    if (filterContext.HttpContext.Request.Cookies[KeyMaintenanceWarningCookie] == null || filterContext.HttpContext.Request.Cookies[KeyMaintenanceWarningCookie] != "1")
                    {
                        var difference = (startTime - DateTime.UtcNow);
                        if (difference.TotalSeconds < warningLead)
                        {
                            _sessionMessageManager.SetMessage(MessageType.Warning, MessageBehaviors.Modal, maintenanceWarningMessage, KeyMaintenanceWarningMessage);
                            filterContext.HttpContext.Response.Cookies.Append(KeyMaintenanceWarningCookie, "1", new CookieOptions { HttpOnly = true });
                        }
                    }
                }
            }
        }
        public void OnActionExecuted(ActionExecutedContext context)
        { }
    }
}
