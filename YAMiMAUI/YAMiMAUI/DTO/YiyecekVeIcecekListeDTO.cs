  using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YAMiMAUI.DTO
{
    public class YiyecekVeIcecekListeDTO
    {
        public Guid YId { get; set; }
        public string YAdi { get; set; }
        public Decimal YSatisF { get; set; }
        public Decimal YAlisF { get; set; }
        public int YMiktari { get; set; }
        public string EkleyenPersonel {  get; set; }
        public DateTime EklenmeTarihi { get; set; }
        public string? GuncelleyenPersonel { get; set; }
        public DateTime? GuncellemeTarihi { get; set; }

    }
    public class YiyecekVeIcecekMListeDTO
    {
        public Guid Yıd { get; set; }
        public string YAdi { get; set; }
        public Decimal YSatisF { get; set; }
        public int YMiktari { get; set; }
    }
    public class YiyecekVeIcecekSilDTO
    {
        public Guid YId { get; set; }
    }
    public class YiyecekVeIcecekEkleDTO
    {
        public string YAdi { get; set; }
        public Decimal YSatisF { get; set; }
        public Decimal YAlis { get; set; }
        public int YMiktari { get; set; }
        public string EkleyenPersonel { get; set; }
        public DateTime EklenmeTarihi { get; set; }
    }
    public class YiyecekVeIcecekGuncelleDTO
    {
        public Guid YId { get; set; }
        public string YAdi { get; set; }
        public Decimal? YSatisF { get; set; }
        public Decimal? YAlis { get; set; }
        public int? YMiktari { get; set; }
        public string EkleyenPersonel {  get; set; }
        public DateTime EklenmeTarihi { get; set; }
        public string? GuncelleyenPersonel { get; set; }
        public DateTime? GuncellemeTarihi { get; set; }
    }

    public class YiyecekVeIcecekDTO
    {
        public Guid YId { get; set; }
        public string YAdi { get; set; }
        public decimal YSatisF { get; set; }
        public decimal YAlis { get; set; }
        public int YMiktari { get; set; }

        public string EkleyenPersonel { get; set; }
        public DateTime EklenmeTarihi { get; set; }

        public string? GuncelleyenPersonel { get; set; }
        public DateTime? GuncellemeTarihi { get; set; }

    }
}
