using Admin.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Globalization;
using User.Models;

namespace TEKKEN_WEB
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
            services.AddLocalization(options => options.ResourcesPath = "Resources");

            services.AddMvc()
                   .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                   .AddDataAnnotationsLocalization();

            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[] { "ko-KR", "en-US" };
                options.SetDefaultCulture(supportedCultures[0])
                    .AddSupportedCultures(supportedCultures)
                    .AddSupportedUICultures(supportedCultures);
            });


            services.AddSingleton<IConfiguration>(Configuration);
            //services.AddSingleton<ILogger>(loggger);
            services.AddHttpContextAccessor();

            services.AddTransient<ITekkenVersionRepository, TekkenVersionRepository>();
            services.AddTransient<ICharacterRepository, CharacterRepository>();
            services.AddTransient<IMoveRepository, MoveRepository>();
            services.AddTransient<IMoveListRepository, MoveListRepository>();
            services.AddTransient<IMoveTypeRepository, MoveTypeRepository>();
            services.AddTransient<IMoveSubTypeRepository, MoveSubTypeRepository>();
            services.AddTransient<IMoveDataRepository, MoveDataRepository>();
            services.AddTransient<IMoveCommandRepository, MoveCommandRepository>();
            services.AddTransient<IMoveTextRepository, MoveTextRepository>();
            services.AddTransient<IStateRepository, StateRepository>();
            services.AddTransient<IStateGroupRepository, StateGroupRepository>();
            services.AddTransient<IHitTypeRepository, HitTypeRepository>();
            services.AddTransient<ITranslateNameRepository, TranslateNameRepository>();
            //services.AddTransient<IBaseRepository, BaseRepository>();
            services.AddTransient<IBaseCharacterRepository, BaseCharacterRepository>();
            services.AddTransient<IBaseDefaultRepository, BaseDefaultRepository>();
            services.AddTransient<IBaseStateGroupRepository, BaseStateGroupRepository>();
            services.AddTransient<IDumpDataRepository, DumpDataRepository>();

            //services.AddSingleton<IVersionRepository>(new )
            //services.AddDbContext<MvcMovieContext>(options =>
            //options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddControllersWithViews();

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
            //app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions
            {
                OnPrepareResponse = ctx =>
                {
                    // Cache static files for 30 days
                    ctx.Context.Response.Headers.Append("Cache-Control", "public,max-age=2592000");
                    ctx.Context.Response.Headers.Append("Expires", DateTime.UtcNow.AddDays(3).ToString("R", CultureInfo.InvariantCulture));
                }
            });
            //ko-KR
            //en-US
            //zh-CN
            //ja-JP
            var supportedCultures = new[] { "ko-KR", "en-US" };
            var localizationOptions = new RequestLocalizationOptions().SetDefaultCulture(supportedCultures[0])
                .AddSupportedCultures(supportedCultures)
                .AddSupportedUICultures(supportedCultures);
            
            app.UseRequestLocalization(localizationOptions);

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                     name: "Admin",
                     pattern: "{area:exists}/{controller=DumpData}/{action=Index}/{id?}");
                
                endpoints.MapControllerRoute(
                     name: "Admin",
                     pattern: "{area:exists}/{controller=MoveCommand}/{action=Index}/{code?}");

                endpoints.MapControllerRoute(
                     name: "User",
                     pattern: "{area:exists}/{controller=MoveList}/{action=Index}/{id?}");

                //endpoints.MapControllerRoute(
                //     name: "default",
                //     pattern: "{controller=Home}/{action=Index}/{id?}");


                //endpoints.MapControllerRoute(
                //     name: "User",
                //     pattern: "{area:exists}/{controller=Move}/{action=Index}/{id?}");

                //endpoints.MapControllerRoute(
                //    name: "Common",
                //    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapAreaControllerRoute(
                    name: "default",
                    areaName: "Admin",
                    pattern: "{area:exists}/{controller=Move}/{action=Index}/{id?}");


            });
            app.UseRequestLocalization();

        }
    }
}
