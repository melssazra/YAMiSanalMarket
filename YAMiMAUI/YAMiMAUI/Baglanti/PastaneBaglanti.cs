using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using YAMiMAUI.DTO;

namespace YAMiMAUI.Baglanti
{
    public class PastaneBaglanti : APIBaglanti
    {
        public async Task<bool> BaglantiTestiYapAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("/api/pastane");
                return response.IsSuccessStatusCode; // 200-299 arası kod dönerse bağlantı var demektir
            }
            catch
            {
                return false; // Hata varsa bağlantı yok
            }
        }

        public async Task<bool> PastaneUrunEkleAsync(PastaneEkleDTO urun)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("/api/pastane/ekle", urun);
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<PastaneListeDTO>> PastaneUrunGetirAsync()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<PastaneListeDTO>>("/api/pastane/liste");
                return response ?? new List<PastaneListeDTO>();
            }
            catch
            {
                return new List<PastaneListeDTO>();
            }
        }


        public async Task<bool> PastaneUrunGuncelleAsync(PastaneGuncelleDTO guncel)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync("/api/pastane/guncelle", guncel);
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }


        public async Task<bool> PastaneUrunSilAsync(Guid id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"/api/pastane/sil/{id}");
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        public async Task<(bool Basarili, string Mesaj)> PastanePUrunGuncelleAsync(Guid id, PastaneDTO dto)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/pastane/guncelle/{id}", dto);
            return (response.IsSuccessStatusCode, response.IsSuccessStatusCode ? "Güncellendi" : await response.Content.ReadAsStringAsync());
        }


    }
}