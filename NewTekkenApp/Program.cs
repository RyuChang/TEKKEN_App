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

//   .AddFacebook(options =>
//   {
//       IConfigurationSection FBAuthNSection =
//       config.GetSection("Authentication:FB");
//       options.ClientId = FBAuthNSection["ClientId"];
//       options.ClientSecret = FBAuthNSection["ClientSecret"];
//   })
//   .AddMicrosoftAccount(microsoftOptions =>
//   {
//       microsoftOptions.ClientId = config["Authentication:Microsoft:ClientId"];
//       microsoftOptions.ClientSecret = config["Authentication:Microsoft:ClientSecret"];
//   })
//   .AddTwitter(twitterOptions =>
//   {
//       twitterOptions.ConsumerKey = config["Authentication:Twitter:ConsumerAPIKey"];
//       twitterOptions.ConsumerSecret = config["Authentication:Twitter:ConsumerSecret"];
//       twitterOptions.RetrieveUserDetails = true;
//   });
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();
builder.Services.AddLocalization();
builder.Services.AddDatabaseDeveloperPageExceptionFilter(); // 개발 환경에 유용한 오류 정보 제공

builder.Services.AddBlazoredModal();

builder.Services.AddSingleton<HttpClient>();

builder.Services.AddSingleton<WeatherForecastService>();

builder.Services.AddTransient<IHitTypeService, HitTypeService>();
builder.Services.AddTransient<IStateService, StateService>();
builder.Services.AddTransient<IStateGroupService, StateGroupService>();
builder.Services.AddTransient<IMoveTextService, MoveTextService>();
builder.Services.AddTransient<IMoveTypeService, MoveTypeService>();
builder.Services.AddTransient<IMoveSubTypeService, MoveSubTypeService>();
builder.Services.AddTransient<IMoveService, MoveService>();
builder.Services.AddTransient<IMoveDataService, MoveDataService>();
builder.Services.AddTransient<IMoveCommandService, MoveCommandService>();

builder.Services.AddTransient<ICommandService, CommandService>();
builder.Services.AddTransient<ICharacterService, CharacterService>();
builder.Services.AddTransient<ILanguageService, LanguageService>();
//builder.Services.AddScoped<ILocalizationService, LocalizationService>();


builder.Services.AddTransient<NavigationUtil>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
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

});
//app.MapFallbackToAreaPage("~/Admin/{*clientroutes:nonfile}", "/Admin/_Host", "Admin");
//app.MapFallbackToAreaPage("/Admin/{*clientroutes:nonfile}", "/_AdminHost", "Admin");

app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");


app.UseCors(policy =>
    policy.WithOrigins("https://localhost:7275;", "http://localhost:5275")
    .AllowAnyMethod()
    .WithHeaders(HeaderNames.ContentType, HeaderNames.Authorization, "x-custom-header")
    .AllowCredentials());

//
//
//

app.Run();
