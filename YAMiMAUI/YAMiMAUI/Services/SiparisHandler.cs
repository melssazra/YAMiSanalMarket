using YAMiMAUI.Baglanti;
using YAMiMAUI.Services;

namespace YAMiMAUI.Services // Services klasöründe tutmak mantıklı
{
    public class SiparisHandler
    {
        private readonly SepetBaglanti _sepetBaglanti;
        private readonly SepetServis _sepetServis;

        public SiparisHandler(SepetBaglanti sepetBaglanti, SepetServis sepetServis)
        {
            _sepetBaglanti = sepetBaglanti;
            _sepetServis = sepetServis;
        }

        public async Task<bool> TamamlaAsync(Guid musteriId, string paymentMethod, DateTime selectedDate, TimeSpan selectedTime)
        {
            // Bağlantı katmanına delege etme
            // Düzeltme: SiparisTamamlaAsync artık gerekli tüm parametreleri alıyor.
            bool siparisBasarili = await _sepetBaglanti.SiparisTamamlaAsync(musteriId, paymentMethod, selectedDate, selectedTime);

            if (siparisBasarili)
            {
                // İşlem başarılıysa sepeti temizle/yenile
                await _sepetServis.SepetiYukleAsync(musteriId);
            }

            return siparisBasarili;
        }
    }
}