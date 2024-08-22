using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using filmprojei.web.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace filmprojei.web
{
    public class Startup
    {

        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

		}

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<MovieContext>(options => 
            options.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                DataSeeding.Seed(app);
            }

            app.UseStaticFiles();//wwwroot klosorunu kullanýma açar
            app.UseRouting();


            //localhost
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name:"default",
                    pattern:"{controller=Home}/{action=Index}/{id?}"
                    );
               // endpoints.MapControllerRoute(
               //     name:"home",
               //     pattern:"",
               //     defaults: new {controller="home",action="index"}
               //     );
               // endpoints.MapControllerRoute(
               //     name: "about",
               //     pattern: "hakkýmýzda",
               //     defaults: new { controller = "home", action = "About" }
               //     );
               // endpoints.MapControllerRoute(
               //     name: "movielist",
               //     pattern: "movies/list",
               //     defaults: new { controller = "Movies", action = "list" }
               //     );
               // endpoints.MapControllerRoute(
               //name: "moviedetails",
               //pattern: "movies/Details",
               //defaults: new { controller = "Movies", action = "Details" }
               //);
            });
        }
    }
}
