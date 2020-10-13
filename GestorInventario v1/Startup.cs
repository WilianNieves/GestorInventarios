using System;
using System.Collections.Generic;
using System.Linq;
using CBusiness.INV_JAC;
using CData.INV_JAC;
using CData.ORACLE;
using CData.SQLSERVER;
using Coravel;
using GestorInventario_v1.Tareas;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace GestorInventario_v1
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
            services.AddControllersWithViews();
            services.AddSingleton<IConfiguration>(Configuration);
            services.AddSingleton<OracleService>();
            services.AddSingleton<SqlServerService>();
            services.AddTransient<B_Inventario_Jac>();
            services.AddSingleton<D_Inventario_Jac>();

            services.AddScheduler();
            services.AddTransient<InventarioJac>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            //app.UseCors("EnabledCORS");

            if (!env.IsDevelopment())
            {
                //app.UseStaticFiles();
                var provider = app.ApplicationServices;
                provider.UseScheduler(scheduler =>
                {
                    scheduler.OnWorker("Tareas");
                    //scheduler.Schedule<InventarioJac>().Hourly();
                    //scheduler.Schedule<InventarioJac>().DailyAtHour(10);
                    //scheduler.Schedule<InventarioJac>().HourlyAt(5);
                    //scheduler.Schedule<InventarioJac>().DailyAtHour(8);
                    scheduler.Schedule<InventarioJac>().EveryFiveMinutes();
                });
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

        }
    }
}
