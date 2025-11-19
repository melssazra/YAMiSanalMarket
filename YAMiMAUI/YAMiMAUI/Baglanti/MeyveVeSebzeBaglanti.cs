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
    public class MeyveVeSebzeBaglanti : APIBaglanti
    {
        public async Task<bool> BaglantiTestiYapAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("/api/meyvevesebze");
                return response.IsSuccessStatusCode; // 200-299 arası kod dönerse bağlantı var demektir
            }
            catch
            {
                return false; // Hata varsa bağlantı yok
            }
        }

        public async Task<bool> MeyveVeSebzeUrunEkleAsync(MeyveVeSebzeEkleDTO urun)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("/api/meyvevesebze/ekle", urun);
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<MeyveVeSebzeListeDTO>> MeyveVeSebzeUrunGetirAsync()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<MeyveVeSebzeListeDTO>>("/api/meyvevesebze/liste");
                return response ?? new List<MeyveVeSebzeListeDTO>();
            }
            catch
            {
                return new List<MeyveVeSebzeListeDTO>();
            }
        }


        public async Task<bool> MeyveVeSebzeUrunGuncelleAsync(MeyveVeSebzeGuncelleDTO guncel)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync("/api/meyvevesebze/guncelle", guncel);
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }


        public async Task<bool> MeyveVeSebzeUrunSilAsync(Guid id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"/api/meyvevesebze/sil/{id}");
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        public async Task<(bool Basarili, string Mesaj)> MeyveVeSebzePUrunGuncelleAsync(Guid id, MeyveVeSebzeDTO dto)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/meyvevesebze/guncelle/{id}", dto);
            return (response.IsSuccessStatusCode, response.IsSuccessStatusCode ? "Güncellendi" : await response.Content.ReadAsStringAsync());
        }


    }
}