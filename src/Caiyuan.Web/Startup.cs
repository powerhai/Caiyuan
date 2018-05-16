using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Caiyuan.DB.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Caiyuan.Web.Services;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Caiyuan.DB.Access;
using Caiyuan.Web.Config;
using System.Diagnostics;
using System.IO;
using Caiyuan.Web.Services.Wechat;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.WeChat;
using System.Text.RegularExpressions;

namespace Caiyuan.Web
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
            services.AddOptions();
            services.Configure<WeChatSettings>(Configuration.GetSection(nameof(WeChatSettings)));

            // Add framework services.
            services.AddDbContext<AppDbContext>(options => {
                options.UseNpgsql(Configuration.GetConnectionString("PostgreSqlConnection")  );
             });
            
            //options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")
            

            services.AddIdentity<User, IdentityRole>(options =>
                {
                    //options.Cookies.ApplicationCookie.AuthenticationScheme = "WechatCookie";
                    //options.Cookies.ApplicationCookie.CookieName = "Interop";
                    
                    //options.Cookies.ApplicationCookie.DataProtectionProvider = DataProtectionProvider.Create(new DirectoryInfo("C:\\Github\\Identity\\artifacts"));
                })
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            services.AddMvc().AddXmlSerializerFormatters();
            services.AddSession();

            // Add application services.
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();
            services.TryAddScoped<Caiyuan.Web.Services.UserManager, Caiyuan.Web.Services.UserManager>();
            services.TryAddScoped<Caiyuan.Web.Services.SignInManager, Caiyuan.Web.Services.SignInManager>();
            services.TryAddScoped<WechatMenuService, WechatMenuService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            //if (env.IsDevelopment())
            //{
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseBrowserLink();
            //}
            //else
            //{
            //    app.UseExceptionHandler("/Home/Error");
            //}

            app.UseStaticFiles();
            
            app.UseIdentity();
            app.UseWeChatAuthentication( new WeChatOptions {
                AppId= "wxecc27ec6dafc530e",
                AppSecret = "502d0c76ecf31c21e453cfcdaf3c08b5"
            });
            app.UseGoogleAuthentication( new GoogleOptions() { ClientId = "w3423423", ClientSecret= "502d0c76ecf31c21e453cfcdaf3c08b5"});
            

            //app.UseCookieAuthentication(new CookieAuthenticationOptions()
            //{
            //    AuthenticationScheme = "Wechat",
            //    LoginPath = new PathString("/Vip/Haiser/"),
            //    AccessDeniedPath = new PathString("/Account/Forbidden/"),
            //    AutomaticAuthenticate = true,
            //    AutomaticChallenge = true
            //});

                //app.UseCookieAuthentication(new CookieAuthenticationOptions()
                //{
                //    AuthenticationScheme = "QQ",
                //    LoginPath = new PathString("/Account/Login/"),
                //    AccessDeniedPath = new PathString("/Account/Forbidden/"),
                //    AutomaticAuthenticate = true,
                //    AutomaticChallenge = true
                //});

                // Add external authentication middleware below. To configure them please see http://go.microsoft.com/fwlink/?LinkID=532715

                app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
                 
            }); 
        }
    }
}
