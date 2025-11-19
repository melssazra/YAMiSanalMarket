using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YAMi_API.Siniflar
{
    public class Sepet
    {
        [Key]
        public Guid SepetId { get; set; }
        public Guid MusteriId { get; set; }

        [ForeignKey("MusteriId")]
        public Musteri Musteri { get; set; }

        public Guid UrunId { get; set; }
        public string UrunTuru { get; set; }
        public int UrunAdet { get; set; }

        public Decimal Tutar { get; set; }

    }
}
