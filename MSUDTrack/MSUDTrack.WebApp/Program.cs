using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.WindowsServices;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MSUDTrack.Services;

namespace MSUDTrack.WebApp
{
    public class Program
    {
        private static IWebHost WebHostInstance;

        public static void Main(string[] args)
        {
            try
            {
                var isService = !(Debugger.IsAttached || args.Contains("--console") || args.Contains("-c"));
                var builder = CreateWebHostBuilder(args.Where(arg => arg != "--console" && arg != "-c").ToArray());

                if (isService)
                {
                    var pathToExe = Process.GetCurrentProcess().MainModule.FileName;
                    var pathToContentRoot = Path.GetDirectoryName(pathToExe);
                    builder.UseContentRoot(pathToContentRoot);
                }

                WebHostInstance = builder.Build();

                var scope = WebHostInstance.Services.CreateScope();

                var seedDataService = scope.ServiceProvider.GetService<SeedDataService>();
                seedDataService.SeedPeriodsAsync(); //Don't hold up the startup process. Seeding will catch up later.

                if (isService && RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                    WebHostInstance.RunAsService();
                else
                    WebHostInstance.Run();
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                //standard MSUD Track port is 32981
                //standard Chrome remote debugging port is 32980
                .UseUrls("https://*:32981", "http://*:32980")
                .UseStartup<Startup>();
    }
}
