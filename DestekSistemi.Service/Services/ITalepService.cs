using DestekSistemi.Entities;
using DestekSistemi.ViewModels; // <-- ViewModels projesine referans eklemen gerekebilir.

namespace DestekSistemi.Service.Services
{
    public interface ITalepService
    {
        Task TalepOlusturAsync(TalepOlusturViewModel viewModel, string kullaniciId);

        // YENİ METOT:

        Task<List<Talep>> GetTaleplerByRoleAsync(string kullaniciId, bool isAdmin);
        Task<Talep> GetTalepByIdAsync(int id);

    }
}