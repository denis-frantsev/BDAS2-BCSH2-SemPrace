using BDAS2_SemPrace.Controllers;
using BDAS2_SemPrace.Data;
using BDAS2_SemPrace.Data.Interfaces;
using BDAS2_SemPrace.Data.Mocks;
using BDAS2_SemPrace.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BDAS2_SemPrace
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        //private IConfigurationRoot _conString;
        private string _conString = "Data Source=(description=(address_list=(address = (protocol = TCP)(host = fei-sql1.upceucebny.cz)(port = 1521)))(connect_data=(service_name=IDAS.UPCEUCEBNY.CZ))\n);User ID=ST64102;Password=j8ex765gh;Persist Security Info=True";

        [System.Obsolete]
        public Startup(Microsoft.Extensions.Hosting.IHostingEnvironment hosting)
        {
            //_conString = new ConfigurationBuilder().SetBasePath(hosting.ContentRootPath).AddJsonFile("dbSettings.json").Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ModelContext>(options => options.UseOracle(_conString));
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseStatusCodePages();
            app.UseStaticFiles();
            app.UseRouting();
            //app.UseMvcWithDefaultRoute();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
