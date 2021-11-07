using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebStore.DAL.Context;
using WebStore.Infrastructura.Conventions;
using WebStore.Infrastructura.Middleware;
using WebStore.Services;
using WebStore.Services.Interfaces;

namespace WebStore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<WebStoreDB>(opt => 
                opt.UseSqlServer(Configuration.GetConnectionString("SqlServer")));
            services.AddSingleton<IEmployeesData, InMemoryEmployeesData>();
            services.AddSingleton<IProductData, InMemoryProductData>();
            services.AddControllersWithViews(opt=>opt.Conventions.Add(new TestControllerConvention()))
                .AddRazorRuntimeCompilation();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }

            app.UseStatusCodePagesWithRedirects("~/Home/Status/{0}");
            app.UseStaticFiles();
            app.UseRouting();

            app.UseMiddleware<TestMiddleware>();

            app.UseWelcomePage("/welcome");

            app.UseEndpoints(endpoints =>
                {
                    endpoints.MapGet("/helloWorld", async context =>
                    {
                        await context.Response.WriteAsync("Hello World!");
                    });

                    endpoints.MapControllerRoute(
                        "default",
                        "{controller=Home}/{action=Index}/{id?}");
                });
        }
    }

}
