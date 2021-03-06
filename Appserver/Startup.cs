using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Appserver.Data;
using Microsoft.EntityFrameworkCore;
using Common.Data;
using Microsoft.Azure.Documents.SystemFunctions;
using Appserver.Controllers;

namespace Appserver
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
            services.AddCors();
            services.AddControllers().AddNewtonsoftJson();

            services.AddDbContext<SubmissionStagingContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("AzureDB"),
                    b => b.MigrationsAssembly("Appserver")));

            services.AddDbContext<SubmissionContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("AzureDB"),
                    b => b.MigrationsAssembly("AdminUI")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            UpdateDatabase(app);
            
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

            app.UseCors(builder =>
              builder.WithOrigins("http://localhost:8080").AllowAnyHeader().AllowAnyMethod());

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                    // Homescreen route
                    endpoints.MapControllerRoute(
                        name: "default",
                        pattern: "{controller=Home}/{action=Index}/");

            });
        }
        private static void UpdateDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<SubmissionStagingContext>())
                {
                    context.Database.Migrate();
                }
            }
        }
    }
}
