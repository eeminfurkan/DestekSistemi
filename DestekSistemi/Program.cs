using DestekSistemi.Data;
using DestekSistemi.DataAccess.Context;
using DestekSistemi.DataAccess.Repositories;
using DestekSistemi.Service.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// DbContext ve Identity'i kendi DbContext'imizle yapýlandýrýyoruz
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString, b => b.MigrationsAssembly("DestekSistemi.DataAccess"))); // Migration'larýn hangi projede olduðunu belirtiyoruz.

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Program.cs

// Program.cs

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>() // <-- YENÝ EKLENEN KISIM
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddControllersWithViews();
builder.Services.AddScoped<ITalepRepository, TalepRepository>();
builder.Services.AddScoped<ITalepService, TalepService>();

// YENÝ EKLENEN SATIRLAR:
builder.Services.AddScoped<IYorumRepository, YorumRepository>();
builder.Services.AddScoped<IYorumService, YorumService>();

var app = builder.Build();

// YENÝ EKLENEN KISIM:
// Veritabaný rollerini ve varsayýlan admin'i oluþturmak için seeder'ý çalýþtýr
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        // DataSeeder'daki metodumuzu çaðýrýyoruz.
        await DataSeeder.SeedRolesAndAdminAsync(services);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Veri tohumlama sýrasýnda bir hata oluþtu.");
    }
}
// ------


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
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
app.MapRazorPages();

app.Run();
