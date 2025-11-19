using Microsoft.AspNetCore.Mvc;
using YAMi_API.DTO;
using YAMi_API.Siniflar;
using YAMi_API.VeriTabani;
using Microsoft.EntityFrameworkCore; // Include'u kullanmak için

namespace YAMi_API.Kontroller
{
    [ApiController]
    [Route("api/sepet")]
    public class SepetKontrol : ControllerBase
    {
        private readonly YAMiDbContext _context;
        public SepetKontrol(YAMiDbContext context) => _context = context;

        [HttpPost("ekle")]
        public IActionResult Ekle(SepetEkleDTO dto)
        {
            var mevcutSepetUrunu = _context.Sepet
                                           .FirstOrDefault(s => s.MusteriId == dto.MusteriId &&
                                                                s.UrunId == dto.UrunId &&
                                                                s.UrunTuru == dto.UrunTuru);

            if (mevcutSepetUrunu != null)
            {
                mevcutSepetUrunu.UrunAdet += dto.UrunAdet;
            }
            else
            {
                var sepet = new Sepet
                {
                    SepetId = Guid.NewGuid(),
                    MusteriId = dto.MusteriId,
                    UrunId = dto.UrunId,
                    UrunTuru = dto.UrunTuru,
                    UrunAdet = dto.UrunAdet
                };
                _context.Sepet.Add(sepet);
            }
            _context.SaveChanges();
            return Ok("Sepete eklendi.");
        }

        [HttpGet("liste")] // musteriId'yi query parametresi olarak al
        public IActionResult Listele([FromQuery] Guid musteriId)
        {
            var sepetItems = _context.Sepet
                                     .Where(s => s.MusteriId == musteriId)
                                     .ToList(); // Bu noktada sepet öğeleri belleğe alınır.

            var liste = sepetItems.Select(s =>
            {
                decimal birimFiyat = 0;
                string urunAdi = "";

                switch (s.UrunTuru)
                {
                    case "Pastane":
                        var pastaneUrun = _context.Pastane.FirstOrDefault(p => p.PId == s.UrunId);
                        if (pastaneUrun != null)
                        {
                            birimFiyat = pastaneUrun.PSatisF;
                            urunAdi = pastaneUrun.PAdi;
                        }
                        break;
                    case "Kozmetik":
                        var kozmetikUrun = _context.Kozmetik.FirstOrDefault(k => k.KId == s.UrunId);
                        if (kozmetikUrun != null)
                        {
                            birimFiyat = kozmetikUrun.KSatisF;
                            urunAdi = kozmetikUrun.KAdi;
                        }
                        break;
                    case "YiyecekVeIcecek":
                        var yiyecekUrun = _context.YiyecekVeIcecek.FirstOrDefault(y => y.YId == s.UrunId);
                        if (yiyecekUrun != null)
                        {
                            birimFiyat = yiyecekUrun.YSatisF;
                            urunAdi = yiyecekUrun.YAdi;
                        }
                        break;
                    case "MeyveVeSebze":
                        var meyveUrun = _context.MeyveVeSebze.FirstOrDefault(m => m.MId == s.UrunId);
                        if (meyveUrun != null)
                        {
                            birimFiyat = meyveUrun.MSatisF;
                            urunAdi = meyveUrun.MAdi;
                        }
                        break;
                }

                return new SepetListeDTO
                {
                    SepetId = s.SepetId,
                    MusteriId = s.MusteriId,
                    UrunId = s.UrunId,
                    UrunTuru = s.UrunTuru,
                    UrunAdi = urunAdi, // Ürün adını da gönder
                    UrunAdet = s.UrunAdet,
                    Tutar = birimFiyat * s.UrunAdet // toplam tutarı
                };
            }).ToList();

            return Ok(liste);
        }

        [HttpPut("guncelle")]
        public IActionResult Guncelle(SepetGuncelleDTO dto)
        {
            var sepet = _context.Sepet.Find(dto.SepetId);
            if (sepet == null) return NotFound("Sepet öğesi bulunamadı.");

            if (dto.UrunAdet.HasValue && dto.UrunAdet.Value >= 0)
            {
                sepet.UrunAdet = dto.UrunAdet.Value;
                _context.SaveChanges();
                return Ok("Sepet güncellendi.");
            }
            else if (dto.UrunAdet.HasValue && dto.UrunAdet.Value < 0)
            {
                return BadRequest("Ürün adedi negatif olamaz.");
            }
            return BadRequest("Güncellenecek adet bilgisi bulunamadı.");
        }

        [HttpDelete("temizle")]
        public IActionResult Temizle([FromQuery] Guid musteriId)
        {
            var sepetItems=_context.Sepet.Where(s => s.MusteriId == musteriId).ToList();
            if (!sepetItems.Any()) return NotFound("Sepet boş veya müşteri bulunamadı");

            _context.Sepet.RemoveRange(sepetItems);
            _context.SaveChanges();
            return Ok("Sepetiniz başarıyla temizlendi");
        }

        [HttpPost("siparisitamamla")]
        public IActionResult SiparisTamamla([FromQuery] Guid musteriId)
        {
            var sepetItems = _context.Sepet.Where(s => s.MusteriId == musteriId).ToList();
            if (!sepetItems.Any()) return BadRequest("Sepetinizde ürün bulunmamaktadır");

            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    foreach (var s in sepetItems)
                    {
                        //Stok güncelleme işlemi
                        switch (s.UrunTuru)
                        {
                            case "Pastane":
                                var pastaneUrun = _context.Pastane.FirstOrDefault(p => p.PId == s.UrunId);
                                if (pastaneUrun != null)
                                {
                                    pastaneUrun.PMiktari -= s.UrunAdet;
                                }
                                break;
                            case "Kozmetik":
                                var kozmetikUrun = _context.Kozmetik.FirstOrDefault(k => k.KId == s.UrunId);
                                if (kozmetikUrun != null)
                                {
                                    kozmetikUrun.KMiktari -= s.UrunAdet;
                                }
                                break;
                            case "YiyecekVeIcecek":
                                var yiyecekUrun = _context.YiyecekVeIcecek.FirstOrDefault(y => y.YId == s.UrunId);
                                if(yiyecekUrun != null)
                                {
                                    yiyecekUrun.YMiktari -= s.UrunAdet;
                                }
                                break;
                            case "MeyveVeSebze":
                                var meyveUrun = _context.MeyveVeSebze.FirstOrDefault(m => m.MId == s.UrunId);
                                if(meyveUrun != null)
                                {
                                    meyveUrun.MMiktari -= s.UrunAdet;
                                }
                                break;

                        }
                    }

                    _context.SaveChanges(); // stok güncellemelerini kaydet

                    // Sepeti temizle
                    _context.Sepet.RemoveRange(sepetItems);
                    _context.SaveChanges();

                    transaction.Commit();
                    return Ok("Siparişiniz başarıyla alınmıştır");
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return StatusCode(500, $"Sipariş tamamlamırken bir hata oluştu: {ex.Message}");
                }
            }
        }

      /*[HttpDelete("sil")]
        public IActionResult Sil(SepetSilDTO dto)
        {
            var sepet = _context.Sepet.Find(dto.SepetId);
            if (sepet == null) return NotFound("Sepet öğesi bulunamadı.");

            _context.Sepet.Remove(sepet);
            _context.SaveChanges();
            return Ok("Ürün sepetten silindi.");
        }

        [HttpDelete("temizle")] // Sepeti tamamen temizlemek için yeni endpoint
        public IActionResult Temizle([FromQuery] Guid musteriId)
        {
            var sepetItems = _context.Sepet.Where(s => s.MusteriId == musteriId).ToList();
            if (!sepetItems.Any()) return NotFound("Sepet boş veya müşteri bulunamadı.");

            _context.Sepet.RemoveRange(sepetItems);
            _context.SaveChanges();
            return Ok("Sepetiniz başarıyla temizlendi.");
        }*/
    }
}