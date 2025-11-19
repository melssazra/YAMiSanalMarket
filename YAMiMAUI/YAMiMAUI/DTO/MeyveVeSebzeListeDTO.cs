using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YAMiMAUI.DTO
{
    public class MeyveVeSebzeListeDTO
    {
        public Guid MId { get; set; }
        public string MAdi { get; set; }
        public Decimal MSatisF { get; set; }
        public Decimal MAlisF { get; set; }
        public int MMiktari { get; set; }
        public string EkleyenPersonel {  get; set; }
        public DateTime EklenmeTarihi { get; set; }
        public string? GuncelleyenPersonel { get; set; }
        public DateTime? GuncellemeTarihi{ get; set; }

    }

    public class MeyveVeSebzeMListeDTO
    {
        public Guid MId { get; set; }
        public string MAdi { get; set; }
        public Decimal MSatisF { get; set; }
        public int MMiktari { get; set; }

    }
    public class MeyveVeSebzeSilDTO
    {
        public Guid MId { get; set; }
    }
    public class MeyveVeSebzeEkleDTO
    {
        public string MAdi { get; set; }
        public Decimal MSatisF { get; set; }
        public Decimal MAlis { get; set; }
        public int MMiktari { get; set; }
        public string EkleyenPersonel { get;set; }
        public DateTime EklenmeTarihi { get; set; }
    }
    public class MeyveVeSebzeGuncelleDTO
    {
        public Guid MId { get; set; }
        public string MAdi { get; set; }
        public Decimal? MSatisF { get; set; }
        public Decimal? MAlis { get; set; }
        public int? MMiktari { get; set; }

        public string EkleyenPersonel { get; set; }
        public DateTime EklenmeTarihi { get; set; }
        public string? GuncelleyenPersonel { get; set; }
        public DateTime? GuncellemeTarihi { get; set; }
    }

    public class MeyveVeSebzeDTO
    {
        public Guid MId { get; set; }
        public string MAdi { get; set; }
        public Decimal? MSatisF { get; set; }
        public Decimal? MAlis { get; set; }
        public int? MMiktari { get; set; }
        public string EkleyenPersonel { get; set; }
        public DateTime EklenmeTarihi { get; set; }
        public string? GuncelleyenPersonel { get; set; }
        public DateTime? GuncellemeTarihi { get; set; }
    }
}
