using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GoGo.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using GoGo.Models;
using GoGo.Services.Contracts;
using GoGo.Services;
using GoGo.Data.Common;

using ViewModels;
using AutoMapper;
using Mapping;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace GoGo
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
            //AutoMapperConfig.RegisterMappings(
            //    typeof(DestViewModel).Assembly);


            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            
            services.AddScoped(typeof(IDestinationService), typeof(DestinationService));
            services.AddScoped(typeof(ICommentsService), typeof(CommentsService));
            services.AddScoped(typeof(IStoriesService), typeof(StoriesService));
            services.AddScoped(typeof(ICourcesService), typeof(CourcesService));
            services.AddScoped(typeof(IGamesService), typeof(GamesService));

            services.AddDbContext<GoDbContext>(options =>
            {
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection"));

                //options.UseOpenIddict();
            });

            services.AddIdentity<GoUser, ApplicationRole>(
                options => options.Stores.MaxLengthForKeys = 128)
                .AddEntityFrameworkStores<GoDbContext>()
                .AddDefaultUI()
                .AddDefaultTokenProviders();

            services.AddAuthentication().AddCookie();//--------

            // Auto Mapper Configurations
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            //AplicationServices
            services.AddScoped(typeof(IRepository<>), typeof(DbRepository<>));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app,
                                IHostingEnvironment env,
                                GoDbContext context,
                                RoleManager<ApplicationRole> roleManager,
                                UserManager<GoUser> userManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
                
            });

            DummyData.Initialize(context, userManager, roleManager).Wait(); //seed here
        }
    }
}
