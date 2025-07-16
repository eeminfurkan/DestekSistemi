using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// DestekSistemi.Entities/Talep.cs

using DestekSistemi.Entities.Enums;
using System.ComponentModel.DataAnnotations;

namespace DestekSistemi.Entities
{
    public class Talep
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Başlık alanı boş bırakılamaz.")]
        [StringLength(100)]
        public string Baslik { get; set; } = string.Empty; // <-- Düzeltme

        [Required(ErrorMessage = "Açıklama alanı boş bırakılamaz.")]
        public string Aciklama { get; set; } = string.Empty; // <-- Düzeltme

        public DateTime OlusturmaTarihi { get; set; } = DateTime.Now;
        public Durum Durum { get; set; } = Durum.Acik;

        // YENİ EKLENEN SATIR:
        [Required]
        public string KullaniciId { get; set; }
        // TODO: Kullanıcı ile ilişki kurulacak
        // public string KullaniciId { get; set; }
            // --- YENİ EKLENEN SATIR ---
            // Navigation Property: Bu talebe ait tüm yorumların bir listesi.
        public virtual ICollection<Yorum> Yorumlar { get; set; } = new List<Yorum>();
        

    }
}