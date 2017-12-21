# What is MaintenanceFilter

An asp.net core action filter library for maintenance message handling.

# Nuget
```xml
Install-Package MaintenanceFilter
```
# Getting started with MaintenanceFilter 

  * Create class MaintenanceSettingProvider that implement interface IMaintenanceSettingProvider
```xml
  * StartTimeUtc: UTC time to start maintenance; Set to default datetime value will disable maintenance detection.
  * EndTimeUtc: UTC time to end maintenance.
  * WarningLeadTime: Time in seconds before the start time to display the maintenance warning message. Set to 0 to disable warning message.
  * MaintenanceWarningMessage: Warning message to display.
  * GetMaintenanceUrl: Maintenance page url.
  * CanByPass: Return true to bypass maintenance detection in certain circumstance.
```
  * Register MaintenanceFilter and SessionMessage in Startup.cs:
```js
            services.AddSingleton<IMaintenanceSettingProvider, MaintenanceSettingProvider>();
            services.AddMvc(options => {
                options.Filters.Add(typeof(MaintenanceActionFilter));
                options.Filters.Add(typeof(AjaxMessagesActionFilter));
            });
            services.AddSessionMessage();
```            
```js            
            app.UseSessionMessage();
```
  * Disable the filter on maintenance controller:
```js
		[Maintenance(Disabled = true)]
```
  * Setup SessionMessage: Please refer to https://github.com/zsu/SessionMessage
```xml
  * Add @addTagHelper *, SessionMessage.UI to _ViewImports.cshtml
  * Add reference to jquery/jqury UI/toastr;
  * Insert <sessionmessage /> after reference to jquery/jqury UI/toastr;
```

# License
All source code is licensed under MIT license - http://www.opensource.org/licenses/mit-license.php
