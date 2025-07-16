using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace DestekSistemi.ViewModels
{
    public class YorumEkleViewModel
    {
        [Required(ErrorMessage = "Yorum içeriği boş bırakılamaz.")]
        public string Icerik { get; set; }

        // Bu yorumun hangi talebe ekleneceğini bilmemiz gerekiyor.
        public int TalepId { get; set; }
    }
}