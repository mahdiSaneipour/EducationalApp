using EP.Core.Interfaces.Admin;
using EP.Core.Interfaces.Course;
using EP.Core.Interfaces.Discount;
using EP.Core.Interfaces.Episode;
using EP.Core.Interfaces.Order;
using EP.Core.Interfaces.User;
using EP.Core.Interfaces.Wallet;
using EP.Core.Services.Admin;
using EP.Core.Services.Course;
using EP.Core.Services.Discount;
using EP.Core.Services.Episode;
using EP.Core.Services.Order;
using EP.Core.Services.User;
using EP.Core.Services.Wallet;
using EP.Core.Tools.RenderViewToString;
using EP.Domain.Interfaces.Course;
using EP.Domain.Interfaces.Discount;
using EP.Domain.Interfaces.Episode;
using EP.Domain.Interfaces.Order;
using EP.Domain.Interfaces.Roles;
using EP.Domain.Interfaces.User;
using EP.Domain.Interfaces.Wallet;
using EP.Infrastructure.Data.Context;
using EP.Infrastructure.Data.Repository.Course;
using EP.Infrastructure.Data.Repository.Discount;
using EP.Infrastructure.Data.Repository.Episode;
using EP.Infrastructure.Data.Repository.Order;
using EP.Infrastructure.Data.Repository.Roles;
using EP.Infrastructure.Data.Repository.User;
using EP.Infrastructure.Data.Repository.Wallet;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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
services.AddScoped<IDiscountServices, DiscountServices>();
services.AddScoped<IEpisodeServices, EpisodeServices>();
services.AddScoped<ICourseServices, CourseServices>();
services.AddScoped<IWalletServices, WalletServices>();
services.AddScoped<IAdminServices, AdminServices>();
services.AddScoped<IOrderServices, OrderServices>();

#endregion

#region Injection In EPDomain

services.AddScoped<IDiscountRepository, DiscountRepository>();
services.AddScoped<IEpisodeRepository, EpisodeRepository>();
services.AddScoped<IWalletRepository, WalletRepository>();
services.AddScoped<ICourseRepository, CourseRepository>();
services.AddScoped<IOrderRepository, OrderRepository>();
services.AddScoped<IUserRepository, UserRepository>();
services.AddScoped<IRoleRepository, RoleRepository>();

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
