namespace YAMi_API.DTO
{
    public class SepetListeDTO
    {
        public Guid SepetId { get; set; }
        public Guid MusteriId { get; set; }
        public Guid UrunId { get; set; }
        public string UrunTuru { get; set; }
        public int UrunAdet { get; set; }
        public Decimal Tutar { get; set; }

        public string UrunAdi { get; set; }
    }

    public class SepetSilDTO
    {
        public Guid SepetId { get; set; }
    }
    public class SepetEkleDTO
    {
        public Guid MusteriId { get; set; }
        public Guid UrunId { get; set; }
        public string UrunTuru { get; set; }
        public int UrunAdet { get; set; }
    }
    public class SepetGuncelleDTO
    {
        public Guid SepetId { get; set; }
        public int? UrunAdet { get; set; }
    }
}
