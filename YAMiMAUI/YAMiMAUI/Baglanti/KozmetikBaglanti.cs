using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using YAMiMAUI.DTO;

namespace YAMiMAUI.Baglanti
{
    public class KozmetikBaglanti:APIBaglanti
    {
        public async Task<bool> BaglantiTestiYapAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("/api/kozmetik");
                return response.IsSuccessStatusCode; // 200-299 arası kod dönerse bağlantı var demektir
            }
            catch
            {
                return false; // Hata varsa bağlantı yok
            }
        }

        public async Task<bool> KozmetikUrunEkleAsync(KozmetikEkleDTO urun)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("/api/kozmetik/ekle", urun);
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<KozmetikListeDTO>> KozmetikUrunGetirAsync()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<KozmetikListeDTO>>("/api/kozmetik/liste");
                return response ?? new List<KozmetikListeDTO>();
            }
            catch
            {
                return new List<KozmetikListeDTO>();
            }
        }


        public async Task<bool> KozmetikUrunGuncelleAsync(KozmetikGuncelleDTO guncel)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync("/api/kozmetik/guncelle", guncel);
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }


        public async Task<bool> KozmetikUrunSilAsync(Guid id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"/api/kozmetik/sil/{id}");
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        public async Task<(bool Basarili, string Mesaj)> KozmetikKUrunGuncelleAsync(Guid id, KozmetikDTO dto)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/kozmetik/guncelle/{id}", dto);
            return (response.IsSuccessStatusCode, response.IsSuccessStatusCode ? "Güncellendi" : await response.Content.ReadAsStringAsync());
        }

    }
}
