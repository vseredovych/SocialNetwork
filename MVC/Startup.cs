using AutoMapper;
using BLL.Mapper;
using BLL.Services;
using DAL;
using DAL.DatabaseConfig;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace MVC
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

            //Mongo Configuration Dependency Injection
            services.Configure<MongoConfiguration>(
            Configuration.GetSection(nameof(MongoConfiguration)));
            services.AddSingleton<IMongoConfiguration>(sp =>
                sp.GetRequiredService<IOptions<MongoConfiguration>>().Value);

            services.AddTransient<IMongoContext, MongoContext>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            // BLL Mapper Dependency INjection
            services.AddSingleton<IMapper>(ObjectsMapper.CreateMapper());

            // Services Dependency Injection
            services.AddTransient<IPostService, PostService>();

            // Controllers Dependency Injection
            services.AddControllers();
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
        }
    }
}
