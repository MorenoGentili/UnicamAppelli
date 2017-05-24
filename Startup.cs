using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using UnicamAppelli.Modello;
using UnicamAppelli.Servizi;

namespace UnicamAppelli
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            
            if (env.IsDevelopment()) {
                CreaDatabaseSeNonEsiste();
            }
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();
            //services.AddScoped(typeof(Database));
            services.AddScoped(typeof(IServizioCorsi), typeof(Database));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
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

        private void CreaDatabaseSeNonEsiste()
        {
            using (Database db = new Database())
            {
                if (db.Database.EnsureCreated())
                {
                    var corso1 = new Corso("Programmazione", "Culmone Rosario");
                    var corso1Appello1 = new Appello { Corso = corso1, DataAppello = new DateTime(2017, 06, 12, 9, 0, 0) };
                    var corso1Appello2 = new Appello { Corso = corso1, DataAppello = new DateTime(2017, 07, 12, 14, 0, 0) };
                    db.Appelli.Add(corso1Appello1);
                    db.Appelli.Add(corso1Appello2);
                    var corso2 = new Corso("Sistemi operativi", "Diletta Romana Cacciagrano");
                    var corso2Appello1 = new Appello { Corso = corso2, DataAppello = new DateTime(2017, 06, 10, 10, 0, 0) };
                    db.Appelli.Add(corso2Appello1);
                    db.SaveChanges();
                }
            }
        }

    }
}
