using Microsoft.AspNetCore.Mvc;
using YAMi_API.DTO;
using YAMi_API.Siniflar;
using YAMi_API.VeriTabani;

namespace YAMi_API.Kontroller
{
    [ApiController]
    [Route("api/pastane")]
    public class PastaneKontrol : ControllerBase
    {
        private readonly YAMiDbContext _context;

        public PastaneKontrol(YAMiDbContext context) => _context = context;

        [HttpPost("ekle")]
        public IActionResult Ekle(PastaneEkleDTO dto)
        {
            var p = new Pastane
            {
                PId = Guid.NewGuid(),
                PAdi = dto.PAdi,
                PSatisF = dto.PSatisF,
                PAlis = dto.PAlis,
                PMiktari = dto.PMiktari,
                EkleyenPersonel = dto.EkleyenPersonel,
                EklenmeTarihi = DateTime.Now
            };
            _context.Pastane.Add(p);
            _context.SaveChanges();
            return Ok("Eklendi");
        }


        [HttpGet("liste")]
        public IActionResult Listele()
        {
            var liste = _context.Pastane.Select(p => new PastaneListeDTO
            {
                PId = p.PId,
                PAdi = p.PAdi,
                PSatisF = p.PSatisF,
                PAlisF = p.PAlis,
                PMiktari = p.PMiktari,
                EkleyenPersonel = p.EkleyenPersonel,
                EklenmeTarihi = p.EklenmeTarihi,
                GuncelleyenPersonel = p.GuncelleyenPersonel,
                GuncellemeTarihi = p.GuncellemeTarihi
            }).ToList();
            return Ok(liste);
        }

        [HttpDelete("sil/{id}")]
        public IActionResult Sil(Guid id)
        {
            var urun = _context.Pastane.Find(id);
            if (urun == null) return NotFound();
            _context.Pastane.Remove(urun);
            _context.SaveChanges();
            return Ok("Silindi");
        }


        [HttpPut("guncelle")]
        public IActionResult Guncelle(PastaneGuncelleDTO dto)
        {
            var urun = _context.Pastane.Find(dto.PId);
            if (urun == null) return NotFound();

            if (dto.PAdi != null) urun.PAdi = dto.PAdi;
            if (dto.PSatisF.HasValue) urun.PSatisF = dto.PSatisF.Value;
            if (dto.PAlis.HasValue) urun.PAlis = dto.PAlis.Value;
            if (dto.PMiktari.HasValue) urun.PMiktari = dto.PMiktari.Value;

            _context.SaveChanges();
            return Ok("Güncellendi");
        }

        [HttpPut("guncelle/{id}")]
        public IActionResult Guncelle(Guid id, [FromBody] PastaneDTO dto)
        {
            var urun = _context.Pastane.Find(id);
            if (urun == null) return NotFound();

            if (dto.PAdi != null) urun.PAdi = dto.PAdi;
            if (dto.PSatisF != default(decimal)) urun.PSatisF = dto.PSatisF;
            if (dto.PAlis != default(decimal)) urun.PAlis = dto.PAlis;
            if (dto.PMiktari != default(int)) urun.PMiktari = dto.PMiktari;
            urun.EkleyenPersonel = urun.EkleyenPersonel ?? "Bilinmiyor";
            urun.GuncelleyenPersonel = dto.GuncelleyenPersonel ?? urun.GuncelleyenPersonel;
            urun.GuncellemeTarihi = DateTime.Now;

            _context.SaveChanges();
            return Ok("Güncellendi");
        }
    }
}
