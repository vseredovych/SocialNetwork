using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using MVC.Core.Database.Config;
using MVC.Database;
using MVC.Interfaces;
using MVC.Services;

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

            //Neo4j Configuration Dependency Injection
            services.Configure<Neo4jConfiguration>(
            Configuration.GetSection(nameof(Neo4jConfiguration)));
            services.AddSingleton<INeo4jConfiguration>(sp =>
                sp.GetRequiredService<IOptions<Neo4jConfiguration>>().Value);
            services.AddTransient<INeo4jContext, Neo4jContext>();

            // BLL Mapper Dependency Injection
            services.AddSingleton<IMapper>(ObjectsMapper.CreateMapper());

            // Services Dependency Injection
            services.AddTransient<IPostService, PostService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IPersonService, PersonService>();

            // Controllers Dependency Injection
            services.AddControllers();


            //
            //services.Configure<IdentityOptions>(options =>
            //{
            //    // Password settings.
            //    options.Password.RequiredUniqueChars = 1;

            //    // Lockout settings.
            //    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            //    options.Lockout.MaxFailedAccessAttempts = 5;
            //    options.Lockout.AllowedForNewUsers = true;

            //    // User settings.
            //    options.User.AllowedUserNameCharacters =
            //    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
            //    options.User.RequireUniqueEmail = true;
            //});

            //services.ConfigureApplicationCookie(options =>
            //{
            //    // Cookie settings
            //    options.Cookie.HttpOnly = true;
            //    options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

            //    options.LoginPath = "/Account/Login";
            //    options.AccessDeniedPath = "/Account/AccessDenied";
            //    options.SlidingExpiration = true;
            //});
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                            .AddCookie(options => //CookieAuthenticationOptions
                {
                    options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
                });
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

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/");
            });
        }
    }
}
