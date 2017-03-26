using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ZenithWebsite.Models;
using ZenithWebsite.Services;
using Newtonsoft.Json;
using AspNet.Security.OpenIdConnect.Primitives;


namespace ZenithWebsite
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets();
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            //made a change here -Rachel
            services.AddDbContext<ZenithContext>(options => {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
                options.UseOpenIddict();
                });
    

            // Add service and create Policy with options
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });

            

            services.AddOpenIddict(options =>
            {
                // Register the Entity Framework stores.
                options.AddEntityFrameworkCoreStores<ZenithContext>();
                // Register the ASP.NET Core MVC binder used by OpenIddict.
                // Note: if you don't call this method, you won't be able to
                // bind OpenIdConnectRequest or OpenIdConnectResponse parameters.
                options.AddMvcBinders();
                // Enable the token endpoint.
                options.EnableTokenEndpoint("/connect/token");
                // Enable the password flow.
                options.AllowPasswordFlow();
                // During development, you can disable the HTTPS requirement.
                options.DisableHttpsRequirement();
            });

            var connection = Configuration["DefaultConnection:ConnectionString"];
            services.AddDbContext<ZenithContext>(options => options.UseSqlite(connection));

            services.AddMvc().AddJsonOptions(options => {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });

            // Add application services.
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();

            // Identity
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ZenithContext>()
                .AddDefaultTokenProviders();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, ZenithContext context)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            //may need to change to IsProduction later when deploying
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseIdentity();

            // Add external authentication middleware below. To configure them please see http://go.microsoft.com/fwlink/?LinkID=532715

            app.UseCors("CorsPolicy");

            // Register the validation middleware, that is used to decrypt
            // the access tokens and populate the HttpContext.User property.
            app.UseOAuthValidation();
            // Register the OpenIddict middleware.
            app.UseOpenIddict();
            app.UseMvcWithDefaultRoute();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            SeedData.Initialize(context);

        }

       //private async void createRolesandUsers(ZenithContext context, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
       // {
       //     // creating Creating Member role    
       //     if (!await roleManager.RoleExistsAsync("Member"))
       //     {
       //         var role = new IdentityRole();
       //         role.Name = "Member";
       //         var roleResult = await roleManager.CreateAsync(role);

       //         // Here we create a Admin super user who will maintain the website                  
       //         var user = new ApplicationUser();
       //         user.UserName = "m";
       //         user.Email = "m@m.c";
       //         string userPWD = "P@$$w0rd";

       //         var chkUser = await userManager.CreateAsync(user, userPWD);

       //         if (chkUser.Succeeded)
       //         {
       //             var result1 = await userManager.AddToRoleAsync(user, "Member");
       //         }
       //     }

       //     // Create first Admin Role and creating a default Admin User   
       //     var adminExists = await roleManager.RoleExistsAsync("Admin");
       //     if (!adminExists)
       //     {
       //         // first we create Admin role
       //         var role = new IdentityRole();
       //         role.Name = "Admin";
       //         var roleResult = await roleManager.CreateAsync(role);

       //         // Here we create a Admin super user who will maintain the website                  
       //         var user = new ApplicationUser();
       //         user.UserName = "ZenithAdmin";
       //         user.Email = "admin@zenith.com";
       //         string userPWD = "!@#123QWEqwe";
       //         // Create an admin user for marking 
       //         var user2 = new ApplicationUser();
       //         user2.UserName = "a";
       //         user2.Email = "a@a.a";
       //         string user2PWD = "P@$$w0rd";

       //         //Add default User to Role Admin  
       //         var chkUser = await userManager.CreateAsync(user, userPWD);
       //         var chkUser2 = await userManager.CreateAsync(user2, user2PWD);
       //         if (chkUser.Succeeded)
       //         {
       //             var result1 = await userManager.AddToRolesAsync(user, new string[] { "Admin", "Member" });
       //             var result2 = await userManager.AddToRoleAsync(user2, "Admin");
       //         }
       //     }
        //}

    }
}
