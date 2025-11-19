using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YAMi_API.DTO;
using YAMi_API.VeriTabani;

namespace YAMi_API.Kontroller
{
    [ApiController]
    [Route("api/personel")]
    public class PersonelKontrol : ControllerBase
    {
        private readonly YAMiDbContext _context;

        public PersonelKontrol(YAMiDbContext context) => _context = context;

        [HttpPost("giris")]
        public IActionResult Giris(PersonelGirisDTO dto)
        {
            var p = _context.Personel
                .FromSqlRaw("SELECT * FROM Personel WHERE PKullaniciAdi = {0} COLLATE Latin1_General_CS_AS AND PSifre = {1} COLLATE Latin1_General_CS_AS", dto.PKullaniciAdi, dto.PSifre)
                .FirstOrDefault();

            if (p == null) return Unauthorized(new { message = "Giriş başarısız" });
            return Ok(new { p.PersonelId, p.PAdSoyad });
        }
    }
}
