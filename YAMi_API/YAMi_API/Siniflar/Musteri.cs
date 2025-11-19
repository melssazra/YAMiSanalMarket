using System.ComponentModel.DataAnnotations;
namespace YAMi_API.Siniflar
{
    public class Musteri
    {
        [Key]
        public Guid MusteriId { get; set; }
        public string MAdSoyad { get; set; }
        public string MKullaniciAdi { get; set; }
        public string MSifre { get; set; }
        public string MEmail { get; set; }
        public string MTelNo { get; set; }
        public string MAdres { get; set; }
    }
}
