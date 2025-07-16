using Microsoft.AspNetCore.Identity;

namespace DestekSistemi.Data
{
    public static class DataSeeder
    {
        public static async Task SeedRolesAndAdminAsync(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            // --- Rol oluşturma kısmı aynı kalıyor ---
            string[] roleNames = { "Admin", "User" };
            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            // --- Admin kullanıcısını bulma veya oluşturma kısmı GÜNCELLENDİ ---
            var adminEmail = "admin@desteksistemi.com";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);

            // YENİ ŞİFREMİZ
            string yeniSifre = "Sifre123.";

            if (adminUser == null)
            {
                // Eğer kullanıcı yoksa, yeni baştan oluştur
                IdentityUser newAdminUser = new IdentityUser()
                {
                    UserName = adminEmail, // <-- DEĞİŞİKLİK BURADA
                    Email = adminEmail,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(newAdminUser, yeniSifre);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(newAdminUser, "Admin");
                }
            }
            else
            {
                // EĞER KULLANICI ZATEN VARSA, ŞİFRESİNİ SIFIRLA
                // Bu, şifrenin her zaman bizim bildiğimiz şifre olmasını garantiler.
                var token = await userManager.GeneratePasswordResetTokenAsync(adminUser);
                await userManager.ResetPasswordAsync(adminUser, token, yeniSifre);
            }
        }
    }
}