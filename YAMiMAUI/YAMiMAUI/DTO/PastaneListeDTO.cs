using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YAMiMAUI.DTO
{
    public class PastaneListeDTO
    {
        public Guid PId { get; set; }
        public string PAdi { get; set; }
        public Decimal PSatisF { get; set; }
        public Decimal PAlisF { get; set; }
        public int PMiktari { get; set; }
        public string EkleyenPersonel { get; set; }
        public DateTime EklenmeTarihi { get; set; }

        public string? GuncelleyenPersonel { get; set; }
        public DateTime? GuncellemeTarihi { get; set; }

    }
    public class PastaneMListeDTO
    {
        public Guid PId { get; set; }
        public string PAdi { get; set; }
        public Decimal PSatisF { get; set; }
      
        public int PMiktari { get; set; }
    }
    public class PastaneSilDTO
    {
        public Guid PId { get; set; }
    }
    public class PastaneEkleDTO
    {
        public string PAdi { get; set; }
        public Decimal PSatisF { get; set; }
        public Decimal PAlis { get; set; }
        public int PMiktari { get; set; }
        public string EkleyenPersonel { get; set; }
        public DateTime EklenmeTarihi { get; set; }
    }
    public class PastaneGuncelleDTO
    {
        public Guid PId { get; set; }
        public string PAdi { get; set; }
        public Decimal? PSatisF { get; set; }
        public Decimal? PAlis { get; set; }
        public int? PMiktari { get; set; }

        public string EkleyenPersonel { get; set; } 
        public DateTime EklenmeTarihi { get; set; }   

        public string? GuncelleyenPersonel { get; set; }
        public DateTime? GuncellemeTarihi { get; set; }

    }

    public class PastaneDTO
    {
        public Guid PId { get; set; }
        public string PAdi { get; set; }
        public decimal PSatisF { get; set; }
        public decimal PAlis { get; set; }
        public int PMiktari { get; set; }

        public string EkleyenPersonel { get; set; }
        public DateTime EklenmeTarihi { get; set; }

        public string? GuncelleyenPersonel { get; set; }
        public DateTime? GuncellemeTarihi { get; set; }

    }
}
