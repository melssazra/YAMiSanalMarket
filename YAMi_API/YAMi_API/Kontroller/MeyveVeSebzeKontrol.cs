using Microsoft.AspNetCore.Mvc;
using YAMi_API.DTO;
using YAMi_API.Siniflar;
using YAMi_API.VeriTabani;

namespace YAMi_API.Kontroller
{
    [ApiController]
    [Route("api/meyvevesebze")]
    public class MeyveVeSebzeKontrol : ControllerBase
    {
        private readonly YAMiDbContext _context;

        public MeyveVeSebzeKontrol(YAMiDbContext context) => _context = context;

        [HttpPost("ekle")]
        public IActionResult Ekle(MeyveVeSebzeEkleDTO dto)
        {
            var m = new MeyveVeSebze
            {
                MId = Guid.NewGuid(),
                MAdi = dto.MAdi,
                MSatisF = dto.MSatisF,
                MAlis = dto.MAlis,
                MMiktari = dto.MMiktari,
                EkleyenPersonel = dto.EkleyenPersonel,
                EklenmeTarihi = DateTime.Now
            };
            _context.MeyveVeSebze.Add(m);
            _context.SaveChanges();
            return Ok("Eklendi");
        }

        [HttpGet("liste")]
        public IActionResult Listele()
        {
            var liste = _context.MeyveVeSebze.Select(m => new MeyveVeSebzeListeDTO
            {
                MId = m.MId,
                MAdi = m.MAdi,
                MSatisF = m.MSatisF,
                MAlisF=m.MAlis,
                MMiktari = m.MMiktari,
                EkleyenPersonel = m.EkleyenPersonel,
                EklenmeTarihi = m.EklenmeTarihi,
                GuncelleyenPersonel = m.GuncelleyenPersonel,
                GuncellemeTarihi = m.GuncellemeTarihi
            }).ToList();
            return Ok(liste);
        }

        [HttpDelete("sil/{id}")]
        public IActionResult Sil(Guid id)
        {
            var urun = _context.MeyveVeSebze.Find(id);
            if (urun == null) return NotFound();
            _context.MeyveVeSebze.Remove(urun);
            _context.SaveChanges();
            return Ok("Silindi");
        }

        [HttpPut("guncelle")]
        public IActionResult Guncelle(MeyveVeSebzeGuncelleDTO dto)
        {
            var urun = _context.MeyveVeSebze.Find(dto.MId);
            if (urun == null) return NotFound();

            if (dto.MAdi != null) urun.MAdi = dto.MAdi;
            if (dto.MSatisF.HasValue) urun.MSatisF = dto.MSatisF.Value;
            if (dto.MAlis.HasValue) urun.MAlis = dto.MAlis.Value;
            if (dto.MMiktari.HasValue) urun.MMiktari = dto.MMiktari.Value;

            _context.SaveChanges();
            return Ok("Güncellendi");
        }

        [HttpPut("guncelle/{id}")]
        public IActionResult Guncelle(Guid id, [FromBody] MeyveVeSebzeDTO dto)
        {
            var urun = _context.MeyveVeSebze.Find(id);
            if (urun == null) return NotFound();

            if (dto.MAdi != null) urun.MAdi = dto.MAdi;
            if (dto.MSatisF != default(decimal)) urun.MSatisF = dto.MSatisF;
            if (dto.MAlis != default(decimal)) urun.MAlis = dto.MAlis;
            if (dto.MMiktari != default(int)) urun.MMiktari = dto.MMiktari;
            urun.EkleyenPersonel = urun.EkleyenPersonel ?? "Bilinmiyor";
            urun.GuncelleyenPersonel = dto.GuncelleyenPersonel ?? urun.GuncelleyenPersonel;
            urun.GuncellemeTarihi = DateTime.Now;

            _context.SaveChanges();
            return Ok("Güncellendi");
        }
    }

}
