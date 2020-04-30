using GFATicketing.Data.DbContext;
using GFATicketing.Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GFATicketingWebAPI
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
            services.AddCors(options => options.AddPolicy("GfaTicketingDefault", builder => builder.AllowAnyOrigin().AllowAnyHeader()));

            //addDbContext
            services.AddDbContext<GFATicketingDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            //configure strongly typed settings object
            var appSettingsSection = Configuration.GetSection("DefaultSettings");

            services.Configure<AppSettings>(appSettingsSection);

            services.AddMvc(/*mvcOptions => mvcOptions.Filters.Add(new CorsHeaderFilter())*/)
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCors("GfaTicketingDefault");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            //app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
