using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jurdoc.Api.Interface;
using Jurdoc.Api.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Oracle.ManagedDataAccess.Client;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Jurdoc.Api.Models;
using AutoMapper.Data;

namespace Jurdoc.Api
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
            services.AddControllersWithViews();
            services.AddTransient<IEscrituraService, EscrituraService>();
            services.AddSingleton<IConfiguration>(Configuration);

            services.AddMvc();

            //var mapper = new Mapper(cfg => {
            //    cfg.AddDataReaderMapping();
            //    cfg.CreateMap<OracleDataReader, Escritura>;
            //});


            //services.AddAutoMapper(typeof(Startup));

            services.AddAutoMapper(cfg =>
            {
                cfg.AddDataReaderMapping();
            });

            //services.AddRazorPagesOptions(options => {
            //    options.Conventions.AddPageRoute("/Escritura/Index", "");
            //});
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

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseWelcomePage();
        }
    }
}
