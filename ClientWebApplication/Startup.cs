using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClientWebApplication.HTTPClient;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ClientWebApplication
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            // Add HttpClient Accessor.
            services.AddSingleton(DefaultHttpClientAccessor =>
            {
                IHttpClientAccessor httpClientAccessor = new DefaultHttpClientAccessor();

                // Make sure this is the first time we set the properties of the HTTPClient.
                if (httpClientAccessor.HttpClient.BaseAddress == null)
                {
                    // Set the base address on the HTTP Client, which in this case is a web api.
                    httpClientAccessor.HttpClient.BaseAddress = new Uri(Configuration["APIUrl"]);

                    // Add the headers to the HTTP Client.
                    // Add any headers here for the API call that are needed.
                    // Typically we are using the same ones for each api call so store them in the 
                    // appsettings.json file.
                    //httpClientAccessor.HttpClient.DefaultRequestHeaders.TryAddWithoutValidation("SomeHeaderName", Configuration["SomeHeaderName"]);
                }

                // Return the HttpClientAccessor.
                return httpClientAccessor;
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/ClientCaller/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=ClientCaller}/{action=Index}/{id?}");
            });
        }
    }
}
