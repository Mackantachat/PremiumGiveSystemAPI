using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace PremiumGiveSystemAPI
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
            services.AddCors();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "PremiumGiveSystemAPI", Version = "v1" });
            });
            services.AddMvc().AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);
            var cultureInfo = new CultureInfo("th-TH");

            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
            services.AddCors(options =>
            {
                //ระบุโดเมน
                options.AddPolicy("AllowSpecificOrigins",
                 builder =>
                 {
                     builder.WithOrigins(
                         "http://example.com",
                         "http://localhost:4200")
                         .AllowAnyHeader()
                         .AllowAnyMethod();
                     //.WithMethods("GET", "POST", "HEAD");
                 });
                //เปิดเข้าถึงได้ทุกโดเมน
                options.AddPolicy("AllowAll",
                 builder =>
                 {
                     builder.AllowAnyOrigin()
                     .AllowAnyHeader()
                     .AllowAnyMethod();
                 });

                /*
                    The browser can skip the preflight request
                    if the following conditions are true:
                    - The request method is GET, HEAD, or POST.
                    - The Content-Type header
                       - application/x-www-form-urlencoded
                       - multipart/form-data
                       - text/plain
                */
            });
       
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie();

          
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PremiumGiveSystemAPI v1"));
            }
            app.UseCors("AllowAll");
            app.UseCors(
               options => options.AllowAnyOrigin()
                                 .AllowAnyHeader()
                                 .AllowAnyMethod()
           );
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllers();
            //});

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
