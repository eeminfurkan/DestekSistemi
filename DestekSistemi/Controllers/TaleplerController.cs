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
    }
}