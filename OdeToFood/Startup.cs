using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OdeToFood.Data;
using OdeToFood.Services;

namespace OdeToFood
{
    public class Startup
    {
        private IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IGreeter, Greeter>();
            // only will work  for in memory list
            // will not work for multiple users 
            services.AddDbContext<OdeToFoodDbContext>
                (options => options.UseSqlServer(_config.GetConnectionString("OdeToFood")));

            //services.AddSingleton<IResturantData, InMemory>();
            services.AddScoped<IResturantData,SqlRestaurantData>();
            services.AddMvc();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, 
                              IHostingEnvironment env,
                              IGreeter greet,
                              ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseNodeModules(env.ContentRootPath);

            app.UseMvc(ConfigureRoutes);

            //app.Use(next =>
            //{
            //    // this is the middleware using a delagate
            //    return async context =>
            //    {
            //        logger.LogInformation("Request incoming");
            //        if (context.Request.Path.StartsWithSegments("/mym"))
            //        {
            //            await context.Response.WriteAsync("Hit");
            //            logger.LogInformation("Request Handled");
            //        }
            //        else
            //        {
            //            await next(context);
            //            logger.LogInformation("Response Outgoing");
            //        }
            //    };
            //    // end of middleware

            //});

            //app.UseWelcomePage(new WelcomePageOptions
            //{ Path="/wp"
            //});

            //app.Run(async (context) =>
            //{
            //    var greeting = greet.GetMessageOfTheDay();
            //    context.Response.ContentType = "text/plain";
            //    await context.Response.WriteAsync($"{greeting} : {env.EnvironmentName}");
            //});
        }

        private void ConfigureRoutes(IRouteBuilder routeBuilder)
        {
            // options
            // /Home/Index
            // admin/Home/Index
            // /Home/Index/id
            // // /Home/Index/id?
            // = are the defaults

            routeBuilder.MapRoute("Default",
                "{controller=Home}/{action=Index}/{id?}");
        }
    }
}
