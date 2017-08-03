using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;


/*
 Program Name : Startup.cs
 Description : this is the web applilcation to register books users like
 Author: Chinatsu Kawakami
 Date: 2nd August 2017
 */

namespace BookReader
{
    public class Startup
    {
        private readonly IConfigurationRoot configuration;

        public Startup()
        {
           
                    configuration = new ConfigurationBuilder()
                                    .AddEnvironmentVariables()
                                    //Throw exception
                                    .AddJsonFile(env.ContentRootPath + "/config.json")
                                    //Throw exception
                                    .AddJsonFile(env.ContentRootPath + "config.development.json", true)
                                    .Build();
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //accept one prameter 
            services.AddTransient<FeatureToggles>(x => new FeatureToggles
            {
                EnableDeveloperExceptions = 
                configuration.GetValue<bool>("FeatureToggles:EnableDeveloperExceptions")
            });
        }



        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app,
            IHostingEnvironment env, 
            ILoggerFactory loggerFactory,
            FeatureToggles features)
        {
            loggerFactory.AddConsole();

            app.UseExceptionHandler("/error.html");

          

            //if (configuration.GetValue<bool>("FeatureToggles:EnableDeveloperExceptions"))
            if(features.EnableDeveloperExceptions)
            {
                app.UseDeveloperExceptionPage();
            }
            //Add the code to show Error handing and diagnostics 
            app.Use(async (context, next) =>
            {
                if (context.Request.Path.Value.Contains("invalid"))
                    throw new Exception("Error!");

                await next();
            });

            //Method name: UseFileServer
            //this extensiton method comes from StaticFile NuGet Package
            //This enables us to access the file in wwwroot and run it on the web browser
            app.UseFileServer();


        }
    }

}
