using System.ComponentModel.DataAnnotations;

namespace YAMiMAUI.Siniflar
{
    public class YiyecekVeIcecek
    {
        public Guid YId { get; set; }

        public string YAdi { get; set; }
        public Decimal YSatisF { get; set; }
        public Decimal YAlis { get; set; }
        public int YMiktari { get; set; }
    }
}
