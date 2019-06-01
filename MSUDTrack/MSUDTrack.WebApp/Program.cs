using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.WindowsServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MSUDTrack.Services.SeedData;

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
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
                seedDataService.SeedPeriodsAsync(); //Don't hold up the startup process. Seeding will catch up later.
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed

                if (isService && RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                    WebHostInstance.RunAsService();
                else
                    WebHostInstance.Run();
            }
            catch (Exception e)
            {
                //logger.Error(e, "Stopped program because of exception");
                throw;
            }
            finally
            {
                // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
                //NLog.LogManager.Shutdown();
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
