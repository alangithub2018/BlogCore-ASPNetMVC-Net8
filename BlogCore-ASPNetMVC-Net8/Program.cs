using BlogCore_ASPNetMVC_Net8.Data;
using BlogCore_ASPNetMVC_Net8.Data.Data.Initializer;
using BlogCore_ASPNetMVC_Net8.Data.Repository;
using BlogCore_ASPNetMVC_Net8.Data.Repository.IRepository;
using BlogCore_ASPNetMVC_Net8.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("SqlConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultUI();
builder.Services.AddControllersWithViews();

// Add WorkContainer to the Dependency Injection Container
builder.Services.AddScoped<IWorkContainer, WorkContainer>();

// Add DBInitializer to the Dependency Injection Container
builder.Services.AddScoped<IDBInitializer, DBInitializer>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

SeedData();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{area=Client}/{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();

void SeedData()
{
    using var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;
    var dbInitializer = services.GetRequiredService<IDBInitializer>();
    dbInitializer.Initialize();
}
