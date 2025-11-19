using System.ComponentModel.DataAnnotations;

namespace YAMi_API.Siniflar
{
    public class Personel
    {
        [Key]
        public Guid PersonelId { get; set; }

        public string PAdSoyad { get; set; }
        public string PKullaniciAdi { get; set; }
        public string PSifre { get; set; }

    }
}
