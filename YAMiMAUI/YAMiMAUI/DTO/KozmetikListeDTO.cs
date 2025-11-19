using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YAMiMAUI.DTO
{
    public class KozmetikListeDTO
    {
        public Guid KId { get; set; }
        public string KAdi { get; set; }
        public Decimal KSatisF { get; set; }
        public Decimal KAlisF { get; set; }
        public int KMiktari { get; set; }
        public string EkleyenPersonel { get; set; }
        public DateTime EklenmeTarihi { get; set; }

        public string? GuncelleyenPersonel { get; set; }
        public DateTime? GuncellemeTarihi { get; set; }
    }
    public class KozmetikEkleDTO
    {
        public string KAdi { get; set; }
        public Decimal KSatisF { get; set; }
        public Decimal KAlis { get; set; }
        public int KMiktari { get; set; }
        public string EkleyenPersonel { get; set; }
        public DateTime EklenmeTarihi { get; set; }
    }

    public class KozmetikMListeDTO
    {
        public Guid KId { get; set; }
        public string KAdi { get; set; }
        public Decimal KSatisF { get; set; }

        public int PMiktari { get; set; }
    }
    public class KozmetikGuncelleDTO
    {
        public Guid KId { get; set; }
        public string KAdi { get; set; }
        public Decimal? KSatisF { get; set; } 
        public Decimal? KAlis { get; set; }
        public int? KMiktari { get; set; }
        public string EkleyenPersonel { get; set; }
        public DateTime EklenmeTarihi { get; set; }

        public string? GuncelleyenPersonel { get; set; }
        public DateTime? GuncellemeTarihi { get; set; }
    }
    public class KozmetikSilDTO
    {
        public Guid KId { get; set; }
    }

    public class KozmetikDTO
    {
        public Guid KId { get; set; }
        public string KAdi { get; set; }
        public decimal KSatisF { get; set; }
        public decimal KAlis { get; set; }
        public int KMiktari { get; set; }

        public string EkleyenPersonel { get; set; }
        public DateTime EklenmeTarihi { get; set; }

        public string? GuncelleyenPersonel { get; set; }
        public DateTime? GuncellemeTarihi { get; set; }

    }
}
