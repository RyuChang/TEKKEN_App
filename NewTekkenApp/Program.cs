using System.Text.Json.Serialization;
using Blazored.Modal;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using NewTekkenApp.Areas.Identity;
using NewTekkenApp.Data;
using NewTekkenApp.Utilities;
using TekkenApp.Data;
using Ljbc1994.Blazor.IntersectionObserver;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDbContext<TekkenDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("tekkenConnection"), b => b.MigrationsAssembly("TekkenApp")).EnableSensitiveDataLogging(), ServiceLifetime.Transient);
builder.Services.AddControllers().AddJsonOptions(x =>
                    {
                        x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                        x.JsonSerializerOptions.WriteIndented = true;
                    }
                    );

builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddAuthentication().AddGoogle(googleOptions =>
{
    var configuration = builder.Configuration;
    googleOptions.ClientId = configuration["Authentication:Google:ClientId"];
    googleOptions.ClientSecret = configuration["Authentication:Google:ClientSecret"];
});

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();
builder.Services.AddLocalization();
builder.Services.AddDatabaseDeveloperPageExceptionFilter(); // 개발 환경에 유용한 오류 정보 제공

builder.Services.AddBlazoredModal();

builder.Services.AddSingleton<HttpClient>();

builder.Services.AddSingleton<WeatherForecastService>();

builder.Services.AddIntersectionObserver();

builder.Services.AddSingleton<ICommanderMapperService, CommanderMapperService>();
builder.Services.AddTransient<IHitTypeService, HitTypeService>();
builder.Services.AddTransient<IStateService, StateService>();
builder.Services.AddTransient<IStateGroupService, StateGroupService>();
builder.Services.AddTransient<IMoveTextService, MoveTextService>();
builder.Services.AddTransient<IMoveTypeService, MoveTypeService>();
builder.Services.AddTransient<IMoveSubTypeService, MoveSubTypeService>();
builder.Services.AddTransient<IMoveService, MoveService>();
builder.Services.AddTransient<IMoveDataService, MoveDataService>();
builder.Services.AddTransient<IMoveCommandService, MoveCommandService>();
builder.Services.AddTransient<IMoveVideoService, MoveVideoService>();

builder.Services.AddTransient<ICharacterService, CharacterService>();
builder.Services.AddTransient<ILanguageService, LanguageService>();
builder.Services.AddTransient<ICommandService, CommandService>();

builder.Services.AddScoped<ClipboardService>();

builder.Services.AddTransient<NavigationUtil>();

builder.Services.AddAntiforgery(options =>
{
    options.HeaderName = "X-XSRF-TOKEN";
    options.Cookie.Name = "__Host-X-XSRF-TOKEN";
    options.Cookie.SameSite = Microsoft.AspNetCore.Http.SameSiteMode.Strict;
    options.Cookie.SecurePolicy = Microsoft.AspNetCore.Http.CookieSecurePolicy.Always;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();
var supportedCultures = new[] { "ko-KR", "en-US" };
var localizationOptions = new RequestLocalizationOptions()
    .SetDefaultCulture(supportedCultures[0])
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures);

app.UseRequestLocalization(localizationOptions);


app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapControllerRoute(
         name: "API",
         pattern: "{area:exists}/{controller=Moves}/{action=Index}/{id?}");

    endpoints.MapControllerRoute(
         name: "API",
         pattern: "{area:exists}/{controller=Commands}/{action=Index}/{id?}");

    endpoints.MapControllerRoute(
         name: "Identity",
         pattern: "{area:exists}/{controller=Identity}/{action=Index}");
    endpoints.MapFallbackToPage("/_Host");

});
//app.MapFallbackToAreaPage("~/Admin/{*clientroutes:nonfile}", "/Admin/_Host", "Admin");
//app.MapFallbackToAreaPage("/Admin/{*clientroutes:nonfile}", "/_AdminHost", "Admin");

app.MapControllers();
app.MapBlazorHub();
//app.MapFallbackToPage("/_Host");


app.UseCors(policy =>
    policy.WithOrigins("https://localhost:8164;", "https://ryuc.duckdns.org:8164/")
    .AllowAnyMethod()
    .WithHeaders(HeaderNames.ContentType, HeaderNames.Authorization, "x-custom-header")
    .AllowCredentials());
app.Run();
