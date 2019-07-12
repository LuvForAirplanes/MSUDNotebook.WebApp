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
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MSUDTrack.Services;
using MSUDTrack.DataModels.Models;
using System.Net;
using Microsoft.AspNetCore.HttpOverrides;
using SolidMapper;
using System.Reflection;

namespace MSUDTrack.WebApp
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
            services.AddDbContext<TrackerDbContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<TrackerDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<ForwardedHeadersOptions>(options =>
            {
                var knownProxies = Configuration.GetSection("HttpServer:KnownProxies")
                                                .GetChildren().ToArray()
                                                .Select(p => p.Value)
                                                .ToArray();

                foreach (var proxy in knownProxies)
                {
                    options.KnownProxies.Add(IPAddress.Parse(proxy));
                }
                options.ForwardedHeaders = ForwardedHeaders.All;
            });

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequiredUniqueChars = 0;

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(4320);
                options.Lockout.MaxFailedAccessAttempts = 10;
                options.Lockout.AllowedForNewUsers = true;
            });

            //if (_env.IsDevelopment())
            //{
            //	services.AddWebpack(
            //		configFile: "webpack.dev.js",
            //		publicPath: "/js/react",
            //		webRoot: "./wwwroot",
            //		logLevel: WebpackLogLevel.Normal
            //	);
            //}

            services.AddRouting(options =>
            {
                options.LowercaseUrls = true;
            });

            //services.AddDbContext<ApplicationDbContext>(options =>
            //    options.UseSqlServer(
            //        Configuration.GetConnectionString("DefaultConnection")));
            //services.AddDefaultIdentity<IdentityUser>()
            //    .AddDefaultUI(UIFramework.Bootstrap4)
            //    .AddEntityFrameworkStores<ApplicationDbContext>();


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddScoped<ChildrensService>();
            services.AddScoped<FoodsService>();
            services.AddScoped<PeriodsService>();
            services.AddScoped<RecordsService>();
            services.AddScoped<SeedDataService>();
            services.AddSolidMapper(new Assembly[] {
                typeof(Startup).GetTypeInfo().Assembly
            });

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc();
        }
    }
}
