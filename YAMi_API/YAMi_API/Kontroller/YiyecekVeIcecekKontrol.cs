using Microsoft.AspNetCore.Mvc;
using YAMi_API.DTO;
using YAMi_API.Siniflar;
using YAMi_API.VeriTabani;

namespace YAMi_API.Kontroller
{
    [ApiController]
    [Route("api/yiyecekveicecek")]
    public class YiyecekVeIcecekKontrol : ControllerBase
    {
        private readonly YAMiDbContext _context;

        public YiyecekVeIcecekKontrol(YAMiDbContext context) => _context = context;

        [HttpPost("ekle")]
        public IActionResult Ekle(YiyecekVeIcecekEkleDTO dto)
        {
            var y = new YiyecekVeIcecek
            {
                YId = Guid.NewGuid(),
                YAdi = dto.YAdi,
                YSatisF = dto.YSatisF,
                YAlis = dto.YAlis,
                YMiktari = dto.YMiktari,
                EkleyenPersonel = dto.EkleyenPersonel,
                EklenmeTarihi = DateTime.Now
            };
            _context.YiyecekVeIcecek.Add(y);
            _context.SaveChanges();
            return Ok("Eklendi");
        }

        [HttpGet("liste")]
        public IActionResult Listele()
        {
            var liste = _context.YiyecekVeIcecek.Select(y => new YiyecekVeIcecekListeDTO
            {
                YId = y.YId,
                YAdi = y.YAdi,
                YSatisF = y.YSatisF,
                YAlis=y.YAlis,
                YMiktari = y.YMiktari,
                EkleyenPersonel = y.EkleyenPersonel,
                EklenmeTarihi = y.EklenmeTarihi,
                GuncelleyenPersonel = y.GuncelleyenPersonel,
                GuncellemeTarihi = y.GuncellemeTarihi
            }).ToList();
            return Ok(liste);
        }

        [HttpDelete("sil/{id}")]
        public IActionResult Sil(Guid id)
        {
            var urun = _context.YiyecekVeIcecek.Find(id);
            if (urun == null) return NotFound();
            _context.YiyecekVeIcecek.Remove(urun);
            _context.SaveChanges();
            return Ok("Silindi");
        }

        [HttpPut("guncelle")]
        public IActionResult Guncelle(YiyecekVeIcecekGuncelleDTO dto)
        {
            var urun = _context.YiyecekVeIcecek.Find(dto.YId);
            if (urun == null) return NotFound();

            if (dto.YAdi != null) urun.YAdi = dto.YAdi;
            if (dto.YSatisF.HasValue) urun.YSatisF = dto.YSatisF.Value;
            if (dto.YAlis.HasValue) urun.YAlis = dto.YAlis.Value;
            if (dto.YMiktari.HasValue) urun.YMiktari = dto.YMiktari.Value;

            _context.SaveChanges();
            return Ok("Güncellendi");
        }

        [HttpPut("guncelle/{id}")]
        public IActionResult Guncelle(Guid id, [FromBody] YiyecekVeIcecekDTO dto)
        {
            var urun = _context.YiyecekVeIcecek.Find(id);
            if (urun == null) return NotFound();

            if (dto.YAdi != null) urun.YAdi = dto.YAdi;
            if (dto.YSatisF != default(decimal)) urun.YSatisF = dto.YSatisF;
            if (dto.YAlis != default(decimal)) urun.YAlis = dto.YAlis;
            if (dto.YMiktari != default(int)) urun.YMiktari = dto.YMiktari;
            urun.EkleyenPersonel = urun.EkleyenPersonel ?? "Bilinmiyor";
            urun.GuncelleyenPersonel = dto.GuncelleyenPersonel ?? urun.GuncelleyenPersonel;
            urun.GuncellemeTarihi = DateTime.Now;

            _context.SaveChanges();
            return Ok("Güncellendi");
        }
    }

}
