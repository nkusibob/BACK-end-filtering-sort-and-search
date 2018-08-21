﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApplication1.Models;
using Microsoft.EntityFrameworkCore;
using DomainPsr03951.Models;
using Newtonsoft.Json.Serialization;
using Microsoft.AspNetCore.Identity;
using WebApplication1.Services;
using WebApplication1.Model;
using WebApplication1.Data;

namespace WebApplication1
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
        
            var connection = @"Server=DESKTOP-3Q1QMSK;Database=psr03951DataBase;Trusted_Connection=True;ConnectRetryCount=0";

            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connection));


            services.AddDbContext<psr03951DataBaseContext>(options => options.UseSqlServer(connection));
            services.AddMvc()
                                .AddJsonOptions(options =>
                                {
                                    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                                });
           
            services.AddIdentity<ApplicationUser, IdentityRole>()
               .AddEntityFrameworkStores<psr03951DataBaseContext>()
               .AddDefaultTokenProviders();
            
            services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>,
      CustomClaimsPrincipalFactory>();
            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();
            return services.BuildServiceProvider();
        }
        //Data Source = DESKTOP - 3Q1QMSK;Initial Catalog = psr03951DataBase; Integrated Security = True

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            
            //app.UseNToastNotify();

           
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
