using caferkaynakblog.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace caferkaynakblog
{
    public class Startup
    {

        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))); //Connection String, appsettingste tanımlandı.
            //services.ConfigureApplicationCookie(opt => opt.LoginPath = "/Account/Login");

            services.AddTransient<IRepository, Repository>();
            services.AddMvc();
            services.AddSession();
            services.AddIdentity<User, IdentityRole>()
                    .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
            services.AddAuthenticationCore();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();
            app.UseSession();
            app.UseStatusCodePages();
            app.UseAuthentication();
            app.UseMvc(routes =>
                    {
                        routes.MapRoute(
                    "Entry",
                    "{year}/{month}/{day}/{id}",
                    new {Controllers="Category", action="Entry"},
                    new {year =@"\d4",month=@"\d2",day=@"\d2"}
                    );
                        routes.MapRoute(
                            name: "default",
                            template: "{controller=Home}/{action=Index}/{id?}");
                    });
            SeedData.Seed(app); //veritabanı çalıştırılması için seeddata classındaki fonksiyonun çalışması.
        }
    }
}
