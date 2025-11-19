using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using YAMiMAUI.Baglanti;
using YAMiMAUI.DTO;

namespace YAMiMAUI.Services
{
    public class SepetServis
    {
        private readonly SepetBaglanti _sepetBaglanti;
        public ObservableCollection<SepetListeDTO> SepetItems { get; private set; } = new ObservableCollection<SepetListeDTO>();

        public event Action SepetDegisti; // Sepet değiştiğinde diğer sayfalara bildirim için

        public SepetServis(SepetBaglanti sepetBaglanti)
        {
            _sepetBaglanti = sepetBaglanti;
        }

        // Sepete ürün ekleme metodu
        public async Task SepeteUrunEkleAsync(Guid musteriId, Guid urunId, string urunTuru, int adet)
        {
            var success = await _sepetBaglanti.SepeteUrunEkleAsync(musteriId, urunId, adet, urunTuru);

            if (success)
            {
                await SepetiYukleAsync(musteriId); // Başarıyla eklendiyse sepeti yeniden yükle
                Console.WriteLine("Sepete başarıyla eklendi ve sepet güncellendi.");
            }
            else
            {
                Console.WriteLine("Sepete ürün eklenirken bir hata oluştu.");
            }
        }

        // Sepetten ürün kaldırma metodu
        public async Task SepettenUrunKaldirAsync(Guid sepetId, Guid musteriId)
        {
            var success = await _sepetBaglanti.SepettenUrunSilAsync(sepetId);

            if (success)
            {
                await SepetiYukleAsync(musteriId); // Başarıyla kaldırıldıysa sepeti yeniden yükle
                Console.WriteLine("Ürün sepetten başarıyla kaldırıldı ve sepet güncellendi.");
            }
            else
            {
                Console.WriteLine("Sepetten ürün kaldırılırken bir hata oluştu.");
            }
        }

        // Sepet miktarını güncelleme metodu
        public async Task SepetMiktariGuncelleAsync(Guid sepetId, Guid musteriId, int yeniAdet)
        {
            var success = await _sepetBaglanti.SepetiGuncelleAsync(sepetId, yeniAdet);
            if (success)
            {
                await SepetiYukleAsync(musteriId);
                Console.WriteLine("Sepet miktarı başarıyla güncellendi.");
            }
            else
            {
                Console.WriteLine("Sepet miktarı güncellenirken bir hata oluştu.");
            }
        }

        // Sepeti tamamen temizleme metodu
        public async Task SepetiTemizleAsync(Guid musteriId)
        {
            var success = await _sepetBaglanti.SepetiTamamenTemizleAsync(musteriId);
            if (success)
            {
                await SepetiYukleAsync(musteriId); // Sepeti boşaltmış olacağız
                Console.WriteLine("Sepet başarıyla temizlendi.");
            }
            else
            {
                Console.WriteLine("Sepet temizlenirken bir hata oluştu.");
            }
        }

        // Belirli bir müşterinin sepetini API'den yükleme metodu
        public async Task SepetiYukleAsync(Guid musteriId)
        {
            var apiSepet = await _sepetBaglanti.SepetListeleAsync(musteriId);
            if (apiSepet != null)
            {
                SepetItems.Clear();
                foreach (var item in apiSepet)
                {
                    SepetItems.Add(item);
                }
                SepetDegisti?.Invoke(); // Sepetin değiştiğini diğer abonelere bildir
                Console.WriteLine($"Sepet yüklendi. Toplam öğe: {SepetItems.Count}");
            }
            else
            {
                SepetItems.Clear(); // Hata durumunda veya boş gelirse sepeti temizle
                SepetDegisti?.Invoke();
                Console.WriteLine("Sepet yüklenirken hata oluştu veya sepet boş geldi.");
            }
        }

        // Sepet toplam tutarını hesaplama metodu
        public decimal ToplamTutariHesapla()
        {
            return SepetItems.Sum(item => item.Tutar); // Tutar zaten kalem toplamı olmalı
        }
    }
}