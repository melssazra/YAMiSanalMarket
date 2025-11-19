using System.ComponentModel.DataAnnotations;

namespace YAMiMAUI.Siniflar
{
    public class Pastane
    {
        public Guid PId { get; set; }
        public string PAdi { get; set; }
        public Decimal PSatisF { get; set; }
        public Decimal PAlis { get; set; }
        public int PMiktari { get; set; }

        public string EkleyenPersonel { get; set; }
        public string? GuncelleyenPersonel { get; set; }

        public DateTime EklenmeTarihi { get; set; }
        public DateTime? GuncellemeTarihi { get; set; }
    }
}
