using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace DestekSistemi.Entities
{
    // IdentityUser'dan kalıtım alarak onu genişletiyoruz.
    public class ApplicationUser : IdentityUser
    {
        [StringLength(50)]
        public string? Ad { get; set; } // Soru işareti, bu alanın boş olabileceğini belirtir.

        [StringLength(50)]
        public string? Soyad { get; set; }
    }
}