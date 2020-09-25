using Elsa.Activities.Email.Extensions;
using Elsa.Activities.Http.Extensions;
using Elsa.Activities.Timers.Extensions;
using Elsa.Dashboard.Extensions;
using Elsa.Persistence.EntityFrameworkCore.DbContexts;
using Elsa.Persistence.EntityFrameworkCore.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Jurdoc.Escrituras.Web.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Hosting;
using Elsa.Extensions;
using Jurdoc.Escrituras.Web.Handlers;
using Jurdoc.Escrituras.Web.Extensions;
using Jurdoc.Escrituras.Web.Interfaces;
using Jurdoc.Escrituras.Web.Services;

namespace Jurdoc.Escrituras.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddSingleton<WeatherForecastService>();

            services.AddTransient<IEscrituraService, EscrituraService>();
            services.AddSingleton<IConfiguration>(Configuration);

            services
                // Add services used for the workflows runtime.
                .AddElsa(elsa => elsa.AddEntityFrameworkStores<SqliteContext>(options => options.UseSqlite(Configuration.GetConnectionString("Sqlite"))))
                // Add services used for the workflows dashboard.
                .AddElsaDashboard()

                .AddHttpActivities(options => options.Bind(Configuration.GetSection("Elsa:Http")))
                .AddEmailActivities(options => options.Bind(Configuration.GetSection("Elsa:Smtp")))
                .AddTimerActivities(options => options.Bind(Configuration.GetSection("Elsa:Timers")))
                .AddEscriturasActivities()
                // Add our liquid handler.
                .AddNotificationHandlers(typeof(LiquidConfigurationHandler))
            ;
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
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseStaticFiles();

            // Add Elsa's middleware to handle HTTP requests to workflows.  
            app.UseHttpActivities();

            app.UseRouting();

            app.UseEndpoints(
                endpoints =>
                {
                    // Blazor stuff.
                    endpoints.MapBlazorHub();
                    endpoints.MapFallbackToPage("/_Host");

                    // Attribute-based routing stuff.
                    endpoints.MapControllers();
                });
        }
    }
}
