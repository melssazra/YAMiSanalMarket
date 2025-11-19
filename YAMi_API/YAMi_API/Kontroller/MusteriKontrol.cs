using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YAMi_API.DTO;
using YAMi_API.Siniflar;
using YAMi_API.VeriTabani;

namespace YAMi_API.Kontroller
{
    [ApiController]
    [Route("api/musteri")]
    public class MusteriKontrol : ControllerBase
    {
        private readonly YAMiDbContext _context;

        public MusteriKontrol(YAMiDbContext context) => _context = context;

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("API calisiyor");
        }

        [HttpPost("kayit")]
        public IActionResult UyeOl(UyeOlDTO dto)
        {
            // Kullanıcı adı veya e-posta zaten var mı kontrol et
            var mevcutMusteri = _context.Musteri
                .FirstOrDefault(m => m.MKullaniciAdi == dto.MKullaniciAdi || m.MEmail == dto.MEmail);

            if (mevcutMusteri != null)
            {
                return Conflict("Bu kullanıcı adı veya e-posta ile zaten bir üyelik mevcut.");
            }

            var m = new Musteri
                {
                    MusteriId = Guid.NewGuid(),
                    MAdSoyad = dto.MAdSoyad,
                    MKullaniciAdi = dto.MKullaniciAdi,
                    MSifre = dto.MSifre,
                    MEmail = dto.MEmail,
                    MTelNo = dto.MTelNo,
                    MAdres = dto.MAdres
                };
                _context.Musteri.Add(m);
                _context.SaveChanges();
                return Ok("Kayıt başarılı.");
            
        }

        [HttpPost("giris")]
        public IActionResult Giris(MusteriGirisDTO dto)
        {
            var m = _context.Musteri
                .FromSqlRaw("SELECT * FROM Musteri WHERE MKullaniciAdi = {0} COLLATE Latin1_General_CS_AS AND MSifre = {1} COLLATE Latin1_General_CS_AS", dto.MKullaniciAdi, dto.MSifre)
                .FirstOrDefault();

            if (m == null) return Unauthorized(new { message = "Hatalı giriş" });
            return Ok(new { m.MusteriId, m.MAdSoyad , m.MKullaniciAdi});
        }

        [HttpPost("sifremiunuttum")]
       public async Task<IActionResult> SifreSifirla([FromBody] SifremiUnuttumDTO dto)
       {
          var musteri = await _context.Musteri
              .FirstOrDefaultAsync(x => x.MKullaniciAdi == dto.MKullaniciAdi && x.MTelNo == dto.MTelNo);

          if (musteri == null)
              return BadRequest("Kullanıcı bulunamadı veya bilgiler eşleşmiyor.");

         string yeniSifre = new Random().Next(1000, 9999).ToString();
               musteri.MSifre = yeniSifre;
          await _context.SaveChangesAsync();

          Console.WriteLine($"[SMS] Yeni Şifre: {yeniSifre} - Tel: {musteri.MTelNo}");

         return Ok($"Yeni şifre: {yeniSifre}\nGönderilen Numara: {musteri.MTelNo}\n Lütfen sonrasında şifrenizi değiştiriniz!");
       }

        [HttpGet("liste")]   //müşteri listelemek için
        public IActionResult TumMusterileriGetir()
        {
            var musteriler = _context.Musteri
                .Select(m => new MusteriListeleDTO
                {
                    MusteriId=m.MusteriId,
                    MAdSoyad =m.MAdSoyad,
                    MKullaniciAdi=m.MKullaniciAdi,
                    MEmail=m.MEmail,
                    MTelNo=m.MTelNo,
                    MAdres=m.MAdres
                }).ToList();

            return Ok(musteriler);
        }


        [HttpGet("bilgi/{id}")]
        public async Task<IActionResult> MusteriBilgiGetir(Guid id)
        {
            var musteri = await _context.Musteri.FindAsync(id);

            if (musteri == null)
                return NotFound("Kullanıcı bulunamadı.");

            return Ok(new MusteriGuncelleDTO
            {
                MKullaniciAdi = musteri.MKullaniciAdi,
                MSifre = musteri.MSifre,
                MAdSoyad = musteri.MAdSoyad,
                MEmail = musteri.MEmail,
                MTelNo = musteri.MTelNo,
                MAdres = musteri.MAdres
            });
        }

        [HttpPut]
        public async Task<IActionResult> Guncelle(MusteriGuncelleDTO dto)
        {
            var musteri = await _context.Musteri.FindAsync(dto.MusteriId);

            if (musteri == null)
                return NotFound("Müşteri bulunamadı.");

            musteri.MKullaniciAdi = dto.MKullaniciAdi;
            musteri.MSifre = dto.MSifre;
            musteri.MAdSoyad = dto.MAdSoyad;
            musteri.MEmail = dto.MEmail;
            musteri.MTelNo = dto.MTelNo;
            musteri.MAdres = dto.MAdres;

            await _context.SaveChangesAsync();

            return Ok("Güncelleme başarılı.");
        }

        [HttpDelete("sil/{id}")]
        public async Task<IActionResult> MusteriSil(Guid id)
        {
            var musteri = await _context.Musteri.FindAsync(id);
            if (musteri == null)
                return NotFound("Müşteri bulunamadı.");

            _context.Musteri.Remove(musteri);
            await _context.SaveChangesAsync();

            return Ok("Müşteri silindi.");
        }



    }

}
