using System.ComponentModel.DataAnnotations;

namespace YAMi_API.Siniflar
{
    public class YiyecekVeIcecek
    {
        [Key]
        public Guid YId { get; set; }

        public string YAdi { get; set; }
        public Decimal YSatisF { get; set; }
        public Decimal YAlis { get; set; }
        public int YMiktari { get; set; }
        public string EkleyenPersonel { get; set; }
        public string? GuncelleyenPersonel { get; set; }

        public DateTime EklenmeTarihi { get; set; }
        public DateTime? GuncellemeTarihi { get; set; }
    }
}
