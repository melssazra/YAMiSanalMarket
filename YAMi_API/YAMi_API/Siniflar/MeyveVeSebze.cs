using System.ComponentModel.DataAnnotations;

namespace YAMi_API.Siniflar
{
    public class MeyveVeSebze
    {
        [Key]
        public Guid MId { get; set; }


        public string MAdi { get; set; }
        public Decimal MSatisF { get; set; }
        public Decimal MAlis { get; set; }
        public int MMiktari { get; set; }
        public string EkleyenPersonel { get; set; }
        public string? GuncelleyenPersonel { get; set; }

        public DateTime EklenmeTarihi { get; set; }
        public DateTime? GuncellemeTarihi { get; set; }
    }
}
