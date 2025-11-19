using System.ComponentModel.DataAnnotations;

namespace YAMiMAUI.Siniflar
{
    public class Personel
    {
        public Guid PersonelId { get; set; }

        public string PAdSoyad { get; set; }
        public string PKullaniciAdi { get; set; }
        public string PSifre { get; set; }

    }
}
