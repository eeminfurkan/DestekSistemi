using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace DestekSistemi.Entities
{
    public class Yorum
    {
        public int Id { get; set; }

        [Required]
        public string Icerik { get; set; } = string.Empty;

        public DateTime OlusturmaTarihi { get; set; } = DateTime.Now;

        // --- İlişkiler ---

        // Yorumu yapan kullanıcının ID'si
        [Required]
        public string KullaniciId { get; set; }

        // Yorumun ait olduğu talebin ID'si
        public int TalepId { get; set; }

        // Navigation Property: Bu yorumun hangi talebe ait olduğunu belirtir.
        public Talep Talep { get; set; }
    }
}
