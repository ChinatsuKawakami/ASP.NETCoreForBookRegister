﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
/*
 Program Name : Program.cs
 Description : this is the web applilcation to register books users like
 Author: Chinatsu Kawakami
 Created Date: 2nd August 2017
 Versiton : 0.0.4 add loginForm.html in Project  
 */
namespace BookReader
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .UseApplicationInsights()
                .Build();

            host.Run();
        }
    }
}
