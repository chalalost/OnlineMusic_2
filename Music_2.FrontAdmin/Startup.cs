using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Music_2.ApiIntegration;
using Music_2.ApiIntegration.Role;
using Music_2.ApiIntegration.User;
using Music_2.Data.Models.User;
using Music_2.FrontAdmin.Hubs;
using Music_2.FrontAdmin.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Music_2.FrontAdmin
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
            services.AddHttpClient();
            services.AddControllersWithViews()
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<LoginRequestValidator>());
            services.AddSignalR(cfg => cfg.EnableDetailedErrors = true);
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Login/Index/";
                    options.AccessDeniedPath = "/User/Forbiden";
                });
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddTransient<IEmailSender, EmailSender>();
            services.AddTransient<IUserApiClient, UserApiClient>();
            services.AddTransient<IRoleApiClient, RoleApiClient>();

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
            }
                );

            IMvcBuilder builder = services.AddRazorPages();
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
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
            app.UseAuthentication();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<SignalR>("/signalr");
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                /*endpoints.MapControllerRoute();*/
            });
        }
    }
}
