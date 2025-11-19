using Microsoft.AspNetCore.Mvc;
using YAMi_API.DTO;
using YAMi_API.Siniflar;
using YAMi_API.VeriTabani;

namespace YAMi_API.Kontroller
{
    [ApiController]
    [Route("api/kozmetik")]
    public class KozmetikKontrol : ControllerBase
    {
        private readonly YAMiDbContext _context;

        public KozmetikKontrol(YAMiDbContext context) => _context = context;

        [HttpPost("ekle")]
        public IActionResult Ekle(KozmetikEkleDTO dto)
        {
            var k = new Kozmetik
            {
                KId = Guid.NewGuid(),
                KAdi = dto.KAdi,
                KSatisF = dto.KSatisF,
                KAlis = dto.KAlis,
                KMiktari = dto.KMiktari,
                EkleyenPersonel = dto.EkleyenPersonel,
                EklenmeTarihi = DateTime.Now
            };
            _context.Kozmetik.Add(k);
            _context.SaveChanges();
            return Ok("Kozmetik eklendi.");
        }

        [HttpGet("liste")]
        public IActionResult Listele()
        {
            var liste = _context.Kozmetik.Select(k => new KozmetikListeDTO
            {
                KId = k.KId,
                KAdi = k.KAdi,
                KSatisF = k.KSatisF,
                KAlisF=k.KAlis,
                KMiktari = k.KMiktari,
                EkleyenPersonel = k.EkleyenPersonel,
                EklenmeTarihi = k.EklenmeTarihi,
                GuncelleyenPersonel = k.GuncelleyenPersonel,
                GuncellemeTarihi = k.GuncellemeTarihi
            }).ToList();
            return Ok(liste);
        }

        [HttpDelete("sil/{id}")]
        public IActionResult Sil(Guid id)
        {
            var urun = _context.Kozmetik.Find(id);
            if (urun == null) return NotFound();
            _context.Kozmetik.Remove(urun);
            _context.SaveChanges();
            return Ok("Silindi");
        }

        [HttpPut("guncelle")]
        public IActionResult Guncelle(KozmetikGuncelleDTO dto)
        {
            var urun = _context.Kozmetik.Find(dto.KId);
            if (urun == null) return NotFound();

            if (dto.KAdi != null) urun.KAdi = dto.KAdi;
            if (dto.KSatisF.HasValue) urun.KSatisF = dto.KSatisF.Value;
            if (dto.KAlis.HasValue) urun.KAlis = dto.KAlis.Value;
            if (dto.KMiktari.HasValue) urun.KMiktari = dto.KMiktari.Value;

            _context.SaveChanges();
            return Ok("Güncellendi.");
        }

        [HttpPut("guncelle/{id}")]
        public IActionResult Guncelle(Guid id, [FromBody] KozmetikDTO dto)
        {
            var urun = _context.Kozmetik.Find(id);
            if (urun == null) return NotFound();

            if (dto.KAdi != null) urun.KAdi = dto.KAdi;
            if (dto.KSatisF != default(decimal)) urun.KSatisF = dto.KSatisF;
            if (dto.KAlis != default(decimal)) urun.KAlis = dto.KAlis;
            if (dto.KMiktari != default(int)) urun.KMiktari = dto.KMiktari;
            urun.EkleyenPersonel = urun.EkleyenPersonel ?? "Bilinmiyor";
            urun.GuncelleyenPersonel = dto.GuncelleyenPersonel ?? urun.GuncelleyenPersonel;
            urun.GuncellemeTarihi = DateTime.Now;

            _context.SaveChanges();
            return Ok("Güncellendi");
        }
    }

}
