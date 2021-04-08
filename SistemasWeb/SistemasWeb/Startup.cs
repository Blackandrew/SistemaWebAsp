using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemasWeb.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Microsoft.Extensions.Hosting;

namespace SistemasWeb
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        //para migrar EntityFrameworkCore\Add-Migration
        //para actualizar la db EntityFrameworkCore\update-database

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            //.AddDefaultUI(UIFramework.Bootstrap4)
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<IdentityUser>()
                  .AddDefaultUI()
                  .AddEntityFrameworkStores<ApplicationDbContext>();
             
            //se activa el nuevo enrutador
            services.AddMvc().AddMvcOptions(options => {
                options.EnableEndpointRouting = false;
            })
                .AddNewtonsoftJson();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

             //app.UseRouting(routes =>
             // {
             //   routes.MapControllerRoute(
             //         name: "default",
             //         template: "{controller=Home}/{action=Index}/{id?}");
             //     routes.MapRazorPages();
             // });


            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();



            //se agrega en nuevo enrutamiento para el area principal
            app.UseRouting();



            app.UseAuthentication();

            app.UseAuthorization();
            app.UseCookiePolicy();

            app.UseMvc(routes =>

            {

                routes.MapRoute(
                   name: "default",
                   template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "Areas",
                    template: "{Areas:exists}/{controller=Home}/{action=Index}/{id?}");
                routes.MapAreaRoute("Principal", "Principal", "{controller=Principal}/{action=Index}/{id?}");
                routes.MapAreaRoute("Categorias", "Categorias", "{controller=Categorias}/{action=''}/{id?}");
            });


           

          
          }

        

        
    }
}
