using EP.Core.Interfaces.User;
using EP.Core.Interfaces.Wallet;
using EP.Core.Services.User;
using EP.Core.Services.Wallet;
using EP.Core.Tools.RenderViewToString;
using EP.Domain.Interfaces.User;
using EP.Domain.Interfaces.Wallet;
using EP.Infrastructure.Data.Context;
using EP.Infrastructure.Data.Repository.User;
using EP.Infrastructure.Data.Repository.Wallet;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

IServiceCollection services = builder.Services;

string EPConnection = builder.Configuration.GetConnectionString("EPConnection");

#region Database

services.AddDbContext<EPContext>(options =>
{
    options.UseSqlServer(EPConnection);
});

#endregion

#region Authentication

services.AddAuthentication(options => {
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
}).AddCookie(options => {
    options.LoginPath = "/Login";
    options.LogoutPath = "/Logout";
    options.ExpireTimeSpan = TimeSpan.FromMinutes(43200);
});

#endregion

#region IoC

#region Injection In EPCore

services.AddScoped<IUserAccountServices, UserAccountServices>();
services.AddScoped<IViewRenderService, RenderViewToString>();
services.AddScoped<IUserPanelServices, UserPanelServices>();
services.AddScoped<IWalletServices, WalletServices>();

#endregion

#region Injection In EPDomain

services.AddScoped<IWalletRepository, WalletRepository>();
services.AddScoped<IUserRepository, UserRepository>();

#endregion

#endregion

#region Razor Page

services.AddRazorPages();

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.MapRazorPages();

app.UseRouting();
app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();
