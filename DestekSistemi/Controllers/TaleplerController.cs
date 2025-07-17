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
        private readonly IYorumService _yorumService; // <-- 1. YENİ EKLENEN ALAN


        // 2. CONSTRUCTOR GÜNCELLENDİ
        public TaleplerController(ITalepService talepService, IYorumService yorumService)
        {
            _talepService = talepService;
            _yorumService = yorumService; // Gelen servis değişkene atanıyor.
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
                YeniDurum = talep.Durum,
                YeniYorum = new YorumEkleViewModel { TalepId = id }

            }; 

            return View(viewModel);
        }

        // TaleplerController.cs

        // Yorum ekleme metodu
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> YorumEkle(YorumEkleViewModel YeniYorum)
        {
            if (ModelState.IsValid)
            {
                var kullaniciId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                await _yorumService.YorumEkleAsync(YeniYorum, kullaniciId);

                // BAŞARI MESAJINI EKLE
                TempData["SuccessMessage"] = "Yorumunuz başarıyla eklendi.";
            }
            else
            {
                // HATA MESAJINI EKLE
                TempData["ErrorMessage"] = "Yorum içeriği boş bırakılamaz.";
            }

            return RedirectToAction("Detay", new { id = YeniYorum.TalepId });
        }


        // Durum güncelleme metodu
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Detay(int Id, Durum YeniDurum)
        {
            await _talepService.UpdateTalepDurumAsync(Id, YeniDurum);

            // BAŞARI MESAJINI EKLE
            TempData["SuccessMessage"] = "Talep durumu başarıyla güncellendi.";

            return RedirectToAction("Detay", new { id = Id });
        }



    }
}