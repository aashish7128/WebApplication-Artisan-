using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using WebApplication_Artisan_.Areas.Identity.Data;
using WebApplication_Artisan_.DataAccessLayer;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("WebApplication_Artisan_ContextDBConnection")
    ?? throw new InvalidOperationException("Connection string 'WebApplication_Artisan_ContextDBConnection' not found.");

builder.Services.AddDbContext<WebApplication_Artisan_ContextDB>(options => options.UseSqlServer(connectionString));
builder.Services.AddDbContext<ArtisanDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<Artisan_User>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<WebApplication_Artisan_ContextDB>();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddSession(options =>
{
    options.Cookie.Name = "WebApplication_Artisan_.Session";
    options.IdleTimeout = TimeSpan.FromMinutes(30);
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication(); // Use authentication before authorization
app.UseAuthorization();
app.UseSession(); // Use session middleware

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
