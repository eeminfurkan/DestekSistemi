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

        public HomeController(ILogger<HomeController> logger, ITalepService talepService) // Deðiþtirildi
        {
            _logger = logger;
            _talepService = talepService; // Eklendi
        }

        public async Task<IActionResult> Index() // async Task<> eklendi
        {
            // Kullanýcý giriþ yapmýþ mý?
            if (User.Identity.IsAuthenticated)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var isAdmin = User.IsInRole("Admin"); // TODO: Admin rolünü daha sonra oluþturacaðýz. Þimdilik hep false dönecek.

                var talepler = await _talepService.GetTaleplerByRoleAsync(userId, isAdmin);

                // Talepleri View'e gönder
                return View(talepler);
            }

            // Giriþ yapmamýþsa, boþ anasayfayý göster.
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