using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Web_Application.Auth;
using Web_Application.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddLogging(builder => builder.AddConsole());

#region Database

string connectionString = builder.Configuration.GetConnectionString("MyDatabase") ?? "";

builder.Services.AddDbContext<AuthDbContext>(o =>
{
    o.UseNpgsql(connectionString);
});

string connectionStringCustomDb = builder.Configuration.GetConnectionString("CustomDatabase") ?? "";

builder.Services.AddDbContext<CustomDbContext>(o =>
{
    o.UseNpgsql(connectionStringCustomDb);
});

#endregion

#region Authentication

builder.Services.AddIdentity<AppUser, IdentityRole>()
    .AddEntityFrameworkStores<AuthDbContext>()
    .AddSignInManager<SignInManager<AppUser>>()
    .AddDefaultTokenProviders();

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

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();