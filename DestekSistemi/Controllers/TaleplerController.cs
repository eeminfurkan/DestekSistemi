using DestekSistemi.Entities.Enums;
using DestekSistemi.Service.Services; // Değişti
using DestekSistemi.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DestekSistemi.Controllers
{
    [Authorize]
    public class TaleplerController : Controller
    {
        private readonly ITalepService _talepService; // Değişti

        public TaleplerController(ITalepService talepService) // Değişti
        {
            _talepService = talepService;
        }

        // GET: /Talepler/Olustur
        public IActionResult Olustur()
        {
            return View();
        }

        // POST: /Talepler/Olustur
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Olustur(TalepOlusturViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                // Tek satırda tüm işi servise devrediyoruz!
                await _talepService.TalepOlusturAsync(viewModel, userId);

                return RedirectToAction("Index", "Home");
            }

            return View(viewModel);
        }

        // Controllers/TaleplerController.cs

        // GET: /Talepler/Detay/5
        public async Task<IActionResult> Detay(int id)
        {
            var talep = await _talepService.GetTalepByIdAsync(id);
            if (talep == null) return NotFound();

            // Güvenlik kontrolü... (Aynen kalıyor)
            var mevcutKullaniciId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var kullaniciAdminMi = User.IsInRole("Admin");
            if (!kullaniciAdminMi && talep.KullaniciId != mevcutKullaniciId) return Forbid();

            // View'e göndereceğimiz ViewModel'i oluşturuyoruz
            var viewModel = new TalepDetayViewModel
            {
                Talep = talep,
                // --- ÇÖZÜM BURADA ---
                // ViewModel'in YeniDurum özelliğine, talebin mevcut durumunu atıyoruz.
                YeniDurum = talep.Durum
            };

            return View(viewModel);
        }

        // POST: /Talepler/Detay/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        // DEĞİŞİKLİK BURADA: Artık ViewModel yerine sadece ihtiyacımız olan parametreleri alıyoruz.
        public async Task<IActionResult> Detay(int Id, Durum YeniDurum)
        {
            // Bu yöntemle ModelState.IsValid kontrolüne hiç ihtiyacımız kalmıyor,
            // çünkü gelen parametreler basit tipler (int, enum) ve karmaşık bir model değiller.

            // Servisi kullanarak talep durumunu güncelle
            await _talepService.UpdateTalepDurumAsync(Id, YeniDurum);

            // İşlem bittikten sonra, aynı detay sayfasına geri yönlendir.
            return RedirectToAction("Detay", new { id = Id });
        }

    }
}