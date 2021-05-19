using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NFL.Models;

namespace NFL
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
            services.AddControllersWithViews().AddNewtonsoftJson(); //support for json
            services.AddMemoryCache();
            services.AddSession();

            /*services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(60 * 5);  //SET TIMEOUT FOR SESSION.--DEFAULT IS 20MIN
                options.Cookie.HttpOnly = false;
                options.Cookie.IsEssential = true;
            });*/

            services.AddRouting(options => options.LowercaseUrls = true);
            services.AddControllersWithViews();
            services.AddDbContext<TeamContext>(options => options.UseSqlServer(Configuration.GetConnectionString("TeamContext")));
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

            app.UseSession(); //set always before routing below

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "",
                    pattern: "{controller=Home}/{action=Index}/conf/{activeConf}/div/{activeDiv}"
                );

                endpoints.MapControllerRoute(
                        name: "default",
                        pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
