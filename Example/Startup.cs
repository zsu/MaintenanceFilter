﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MaintenanceFilter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SessionMessage.Core;
using SessionMessage.UI;

namespace Example
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IMaintenanceSettingProvider, MaintenanceSettingProvider>();
            services.AddMvc(options => {
                options.Filters.Add(typeof(MaintenanceActionFilter));
                options.Filters.Add(typeof(AjaxMessagesActionFilter));
            }).AddApplicationPart(typeof(MaintenanceFilterController).Assembly).AddControllersAsServices();
            services.AddSessionMessage();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseForwardedHeaders();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseSessionMessage();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "areaRoute",
                    pattern: "{area:exists}/{controller}/{action}",
                    defaults: new { action = "Index" });
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
