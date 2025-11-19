using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YAMiMAUI.DTO
{
    public class MusteriGirisDTO
    {
        public string MKullaniciAdi { get; set; }
        public string MSifre { get; set; }
        /*public string MAdSoyad { get; internal set; }
        public string MEmail { get; internal set; }
        public string MTelNo { get; internal set; }
        public string MAdres { get; internal set; }*/
    }

    public class MusteriListeleDTO
    {
        public Guid MusteriId { get; set; }
        public string MKullaniciAdi { get; set; }
        public string MSifre { get; set; }
        public string MAdSoyad { get; set; }
        public string MEmail { get;  set; }
        public string MTelNo { get; set; }
        public string MAdres { get; set; }
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

    public class SifremiUnuttumDTO
    {
        public string MKullaniciAdi { get; set; }
        public string MTelNo { get; set; }
    }

    public class MusteriGirisResponseDTO
    {
        public Guid MusteriId { get; set; }
        public string MKullaniciAdi { get; set; }
        
    }
}
