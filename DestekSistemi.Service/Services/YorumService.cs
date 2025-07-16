using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DestekSistemi.DataAccess.Repositories;
using DestekSistemi.Entities;
using DestekSistemi.ViewModels;

namespace DestekSistemi.Service.Services
{
    public class YorumService : IYorumService
    {
        private readonly IYorumRepository _yorumRepository;

        public YorumService(IYorumRepository yorumRepository)
        {
            _yorumRepository = yorumRepository;
        }

        public async Task YorumEkleAsync(YorumEkleViewModel viewModel, string kullaniciId)
        {
            var yeniYorum = new Yorum
            {
                Icerik = viewModel.Icerik,
                TalepId = viewModel.TalepId,
                KullaniciId = kullaniciId,
                OlusturmaTarihi = DateTime.Now
            };

            await _yorumRepository.AddAsync(yeniYorum);
        }
    }
}
