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

        // YENİ METOT: Yorum ekleme formu post edildiğinde çalışır.
        // TaleplerController.cs

        [HttpPost]
        [ValidateAntiForgeryToken]
        // DEĞİŞİKLİK BURADA: Parametre adını "viewModel" yerine "YeniYorum" yapıyoruz.
        public async Task<IActionResult> YorumEkle(YorumEkleViewModel YeniYorum)
        {
            // Artık "YeniYorum" parametresinin içi formdan gelen verilerle doğru bir şekilde dolacak.
            if (ModelState.IsValid)
            {
                var kullaniciId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                // Servise de güncellenmiş parametreyi gönderiyoruz.
                await _yorumService.YorumEkleAsync(YeniYorum, kullaniciId);
            }

            // Yönlendirme de artık doğru ID'yi kullanacak.
            return RedirectToAction("Detay", new { id = YeniYorum.TalepId });
        }


    }
}