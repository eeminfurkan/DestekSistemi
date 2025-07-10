using DestekSistemi.ViewModels; // <-- ViewModels projesine referans eklemen gerekebilir.

namespace DestekSistemi.Service.Services
{
    public interface ITalepService
    {
        Task TalepOlusturAsync(TalepOlusturViewModel viewModel, string kullaniciId);
    }
}