using DestekSistemi.DataAccess.Repositories;
using DestekSistemi.Entities;
using DestekSistemi.ViewModels;

namespace DestekSistemi.Service.Services
{
    public class TalepService : ITalepService
    {
        private readonly ITalepRepository _talepRepository;

        public TalepService(ITalepRepository talepRepository)
        {
            _talepRepository = talepRepository;
        }

        public async Task TalepOlusturAsync(TalepOlusturViewModel viewModel, string kullaniciId)
        {
            // Burası iş mantığının (business logic) merkezidir.
            var yeniTalep = new Talep
            {
                Baslik = viewModel.Baslik,
                Aciklama = viewModel.Aciklama,
                KullaniciId = kullaniciId,
                Durum = Entities.Enums.Durum.Acik,
                OlusturmaTarihi = DateTime.Now
            };

            // Veritabanı işlemi için repository'i kullanıyoruz.
            await _talepRepository.AddAsync(yeniTalep);

            // Örneğin, talep oluşturulunca email gönderme kodu buraya eklenebilir.
        }

        // YENİ METOT:
        public async Task<List<Talep>> GetTaleplerByRoleAsync(string kullaniciId, bool isAdmin)
        {
            if (isAdmin)
            {
                // Eğer kullanıcı admin ise, tüm talepleri getir.
                return await _talepRepository.GetAllAsync();
            }
            else
            {
                // Değilse, sadece o kullanıcının taleplerini getir.
                return await _talepRepository.GetAllByUserIdAsync(kullaniciId);
            }
        }

        // YENİ METOT:
        public async Task<Talep> GetTalepByIdAsync(int id)
        {
            return await _talepRepository.GetByIdAsync(id);
        }
    }
}