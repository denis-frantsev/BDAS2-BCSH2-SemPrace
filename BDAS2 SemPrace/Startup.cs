using BDAS2_SemPrace.Data;
using BDAS2_SemPrace.Data.Interfaces;
using BDAS2_SemPrace.Data.Mocks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using IHostingEnvironment = Microsoft.Extensions.Hosting.IHostingEnvironment;

namespace BDAS2_SemPrace
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        private IConfigurationRoot _conString;
        public Startup(IHostingEnvironment hosting)
        {
            _conString = new ConfigurationBuilder().SetBasePath(hosting.ContentRootPath).AddJsonFile("dbSettings.json").Build();

        }

        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddDbContext<DBContent>(options => options.UseSqlServer());
            services.AddTransient<IZakaznik, MockZakaznik>();
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
