using System.ComponentModel.DataAnnotations;

namespace YAMiMAUI.Siniflar
{
    public class Kozmetik
    {
        public Guid KId { get; set; } //Primary Key olarak tanimlamak icin

        public string KAdi { get; set; }
        public Decimal KSatisF { get; set; }
        public Decimal KAlis { get; set; }
        public int KMiktari { get; set; }

        public string EkleyenPersonel { get; set; }
        public string? GuncelleyenPersonel { get; set; }

        public DateTime EklenmeTarihi { get; set; }
        public DateTime? GuncellemeTarihi { get; set; }

    }
}
