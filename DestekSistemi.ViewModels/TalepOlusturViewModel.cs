using System.ComponentModel.DataAnnotations;

namespace DestekSistemi.ViewModels
{
    public class TalepOlusturViewModel
    {
        [Required(ErrorMessage = "Başlık alanı boş bırakılamaz.")]
        [StringLength(100)]
        public string Baslik { get; set; }

        [Required(ErrorMessage = "Açıklama alanı boş bırakılamaz.")]
        public string Aciklama { get; set; }
    }
}