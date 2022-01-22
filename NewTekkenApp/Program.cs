using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NewTekkenApp.Areas.Identity;
using NewTekkenApp.Data;
using NewTekkenApp.Utilities;
using TekkenApp.Data;
using TekkenApp.Models;
using Blazored.Modal;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDbContext<TekkenDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("tekkenConnection"), b => b.MigrationsAssembly("TekkenApp")).EnableSensitiveDataLogging(), ServiceLifetime.Transient);


builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddBlazoredModal();
builder.Services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();
builder.Services.AddSingleton<WeatherForecastService>();

builder.Services.AddTransient<HitTypeService<HitType, HitType_name>>();
builder.Services.AddTransient<MoveService<Move, Move_name>>();
builder.Services.AddTransient<MoveDataService<MoveData, MoveData_name>>();
builder.Services.AddTransient<MoveCommandService<MoveCommand, MoveCommand_name>>();
builder.Services.AddTransient<CommandService<Command, Command_name>>();
builder.Services.AddTransient<MoveTextService<MoveText, MoveText_name>>();
builder.Services.AddTransient<CharacterService<Character, Character_name>>();
builder.Services.AddTransient<StateService<State, State_name>>();
builder.Services.AddTransient<StateGroupService<StateGroup, StateGroup_name>>();
builder.Services.AddTransient<MoveTypeService<MoveType, MoveType_name>>();
builder.Services.AddTransient<MoveSubTypeService<MoveSubType, MoveSubType_name>>();

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
