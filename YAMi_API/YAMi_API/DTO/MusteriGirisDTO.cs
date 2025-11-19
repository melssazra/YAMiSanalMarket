namespace YAMi_API.DTO
{
    public class MusteriGirisDTO
    {
        public string MKullaniciAdi { get; set; }
        public string MSifre { get; set; }
    }
    public class UyeOlDTO
    {
        public string MAdSoyad { get; set; }
        public string MKullaniciAdi { get; set; }
        public string MSifre { get; set; }
        public string MEmail { get; set; }
        public string MTelNo { get; set; }
        public string MAdres { get; set; }
    }
    public class MusteriSilDTO
    {
        public Guid MusteriId { get; set; }
    }
    public class MusteriGuncelleDTO
    {
        public Guid MusteriId { get; set; }
        public string MAdSoyad { get; set; }
        public string MKullaniciAdi { get; set; }
        public string MSifre { get; set; }
        public string MEmail { get; set; }
        public string MTelNo { get; set; }
        public string MAdres { get; set; }
    }

    public class MusteriListeleDTO
    {
        public Guid MusteriId { get; set; }
        public string MKullaniciAdi { get; set; }
        public string MSifre { get; set; }
        public string MAdSoyad { get;  set; }
        public string MEmail { get;  set; }
        public string MTelNo { get;  set; }
        public string MAdres { get;  set; }
    }
    public class SifremiUnuttumDTO
    {
        public string MKullaniciAdi { get; set; }
        public string MTelNo { get; set; }
    }

}
