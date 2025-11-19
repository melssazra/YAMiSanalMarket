using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;
using YAMiMAUI.DTO;

namespace YAMiMAUI.Baglanti
{
    public class SepetBaglanti : APIBaglanti
    {
        public async Task<bool> SepeteUrunEkleAsync(Guid musteriId, Guid urunId, int adet, string urunTuru)
        {
            var model = new SepetEkleDTO
            {
                MusteriId = musteriId,
                UrunId = urunId,
                UrunAdet = adet,
                UrunTuru = urunTuru
            };

            var response = await _httpClient.PostAsJsonAsync("/api/sepet/ekle", model);
            return response.IsSuccessStatusCode;
        }

        public async Task<List<SepetListeDTO>> SepetListeleAsync(Guid musteriId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/api/sepet/liste?musteriId={musteriId}");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<SepetListeDTO>>();
                }
                else
                {
                    Console.WriteLine($"Sepet listeleme hatası: {response.StatusCode} - {await response.Content.ReadAsStringAsync()}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Sepet listeleme sırasında bir hata oluştu: {ex.Message}");
            }
            return null;
        }

        public async Task<bool> SepettenUrunSilAsync(Guid sepetId)
        {
            var model = new SepetSilDTO { SepetId = sepetId };
            var request = new HttpRequestMessage(HttpMethod.Delete, "/api/sepet/sil");
            request.Content = JsonContent.Create(model);
            var response = await _httpClient.SendAsync(request);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> SepetiGuncelleAsync(Guid sepetId, int yeniAdet)
        {
            var model = new SepetGuncelleDTO { SepetId = sepetId, UrunAdet = yeniAdet };
            var response = await _httpClient.PutAsJsonAsync("/api/sepet/guncelle", model);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> SepetiTamamenTemizleAsync(Guid musteriId)
        {
            var response = await _httpClient.DeleteAsync($"/api/sepet/temizle?musteriId={musteriId}");
            return response.IsSuccessStatusCode;
        }

        // Düzeltme: Teslimat ve ödeme parametreleri eklendi.
        public async Task<bool> SiparisTamamlaAsync(Guid musteriId, string paymentMethod, DateTime selectedDate, TimeSpan selectedTime)
        {
            try
            {
                // Tarih ve saati birleştirip API'ye uygun formatta gönderiyoruz.
                DateTime deliveryDateTime = selectedDate.Date.Add(selectedTime);
                string deliveryDateTimeIso = deliveryDateTime.ToString("o"); // ISO 8601 formatı

                // Örnek olarak QueryString ile gönderim yapıldı.
                var response = await _httpClient.PostAsync(
                    $"/api/sepet/siparisitamamla?musteriId={musteriId}&paymentMethod={paymentMethod}&deliveryDateTime={deliveryDateTimeIso}", null);

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Sipariş tamamlama sırasında bir hata oluştu: {ex.Message}");
                return false;
            }
        }
    }
}