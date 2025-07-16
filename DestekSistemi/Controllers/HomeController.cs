using DestekSistemi.Models;
using DestekSistemi.Service.Services; // Eklendi
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims; // Eklendi

namespace DestekSistemi.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ITalepService _talepService; // Eklendi

        public HomeController(ILogger<HomeController> logger, ITalepService talepService) // De�i�tirildi
        {
            _logger = logger;
            _talepService = talepService; // Eklendi
        }

        public async Task<IActionResult> Index() // async Task<> eklendi
        {
            // Kullan�c� giri� yapm�� m�?
            if (User.Identity.IsAuthenticated)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var isAdmin = User.IsInRole("Admin"); // TODO: Admin rol�n� daha sonra olu�turaca��z. �imdilik hep false d�necek.

                var talepler = await _talepService.GetTaleplerByRoleAsync(userId, isAdmin);

                // Talepleri View'e g�nder
                return View(talepler);
            }

            // Giri� yapmam��sa, bo� anasayfay� g�ster.
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}