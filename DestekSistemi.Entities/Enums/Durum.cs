using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

// DestekSistemi.Entities/Enums/Durum.cs

namespace DestekSistemi.Entities.Enums
{
    public enum Durum
    {
        [Display(Name = "Açık")]
        Acik,     // Açık
        [Display(Name = "İşlemde")]
        Islemde,  // İşlemde
        [Display(Name = "Kapandı")]
        Kapandi   // Kapandı
    }
}
