using DestekSistemi.Data;
using DestekSistemi.DataAccess.Context;
using DestekSistemi.DataAccess.Repositories;
using DestekSistemi.Service.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// DbContext ve Identity'i kendi DbContext'imizle yap�land�r�yoruz
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString, b => b.MigrationsAssembly("DestekSistemi.DataAccess"))); // Migration'lar�n hangi projede oldu�unu belirtiyoruz.

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Program.cs

// Program.cs

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>() // <-- YEN� EKLENEN KISIM
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddControllersWithViews();
builder.Services.AddScoped<ITalepRepository, TalepRepository>();
builder.Services.AddScoped<ITalepService, TalepService>();

// YEN� EKLENEN SATIRLAR:
builder.Services.AddScoped<IYorumRepository, YorumRepository>();
builder.Services.AddScoped<IYorumService, YorumService>();

var app = builder.Build();

// YEN� EKLENEN KISIM:
// Veritaban� rollerini ve varsay�lan admin'i olu�turmak i�in seeder'� �al��t�r
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        // DataSeeder'daki metodumuzu �a��r�yoruz.
        await DataSeeder.SeedRolesAndAdminAsync(services);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Veri tohumlama s�ras�nda bir hata olu�tu.");
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
