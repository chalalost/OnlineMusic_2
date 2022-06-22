using LazZiya.ExpressLocalization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Music_2.ApiIntegration;
using Music_2.ApiIntegration.Category;
using Music_2.ApiIntegration.FeedBack;
using Music_2.ApiIntegration.Language;
using Music_2.ApiIntegration.Product;
using Music_2.ApiIntegration.Slide;
using Music_2.ApiIntegration.User;
using Music_2.Front.Hubs;
using Music_2.Front.LocalizedResources;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Music_2.Front
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
            var cultures = new[]
           {
                new CultureInfo("en"),
                new CultureInfo("vi"),
            };
            services.AddControllersWithViews().AddExpressLocalization<ExpressLocalizationResource, ViewLocalizationResource>(ops =>
            {
                // When using all the culture providers, the localization process will
                // check all available culture providers in order to detect the request culture.
                // If the request culture is found it will stop checking and do localization accordingly.
                // If the request culture is not found it will check the next provider by order.
                // If no culture is detected the default culture will be used.

                // Checking order for request culture:
                // 1) RouteSegmentCultureProvider
                //      e.g. http://localhost:1234/tr
                // 2) QueryStringCultureProvider
                //      e.g. http://localhost:1234/?culture=tr
                // 3) CookieCultureProvider
                //      Determines the culture information for a request via the value of a cookie.
                // 4) AcceptedLanguageHeaderRequestCultureProvider
                //      Determines the culture information for a request via the value of the Accept-Language header.
                //      See the browsers language settings

                // Uncomment and set to true to use only route culture provider
                ops.UseAllCultureProviders = false;
                ops.ResourcesPath = "LocalizationResources";
                ops.RequestLocalizationOptions = o =>
                {
                    o.SupportedCultures = cultures;
                    o.SupportedUICultures = cultures;
                    o.DefaultRequestCulture = new RequestCulture("en");
                };
            });
            services.AddCors(option => option.AddPolicy("CorsPolicy", builder =>
            {
                builder
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowAnyOrigin()
                    .AllowCredentials()
                    .WithOrigins("http://localhost:19401");
            }));
            services.AddSignalR(cfg => cfg.EnableDetailedErrors = true);

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
            });

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Login/Index/";
                    options.AccessDeniedPath = "/User/Forbiden";

                });

            //khai bao services
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<ICategoryApiClient, CategoryApiClient>();
            services.AddTransient<ILanguageApiClient, LanguageApiClient>();
            services.AddTransient<IProductApiClient, ProductApiClient>();
            services.AddTransient<ISlideApiClient, SlideApiClient>();
            services.AddTransient<IFeedBackApiClient, FeedBackApiClient>();
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

            app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true)
                .AllowCredentials());

            app.UseRouting();

            app.UseAuthorization();
            app.UseSession();
            app.UseRequestLocalization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<SignalR>("/signalr");
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{culture=en}/{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
