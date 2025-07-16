using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DestekSistemi.ViewModels; // Bu using gerekli olacak

namespace DestekSistemi.Service.Services
{
    public interface IYorumService
    {
        Task YorumEkleAsync(YorumEkleViewModel viewModel, string kullaniciId);
    }
}