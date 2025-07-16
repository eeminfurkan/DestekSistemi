using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DestekSistemi.Entities;
using DestekSistemi.Entities.Enums;

namespace DestekSistemi.ViewModels
{
    public class TalepDetayViewModel
    {
        // Detaylarını göstereceğimiz talep
        public Talep Talep { get; set; }

        // Admin'in formu post ettiğinde seçtiği yeni durum bu property'de tutulacak
        public Durum YeniDurum { get; set; }
    }
}