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
    public class YiyecekVeIcecekBaglanti : APIBaglanti
    {
        public async Task<bool> BaglantiTestiYapAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("/api/yiyecekveicecek");
                return response.IsSuccessStatusCode; // 200-299 arası kod dönerse bağlantı var demektir
            }
            catch
            {
                return false; // Hata varsa bağlantı yok
            }
        }

        public async Task<bool> YiyecekVeIcecekUrunEkleAsync(YiyecekVeIcecekEkleDTO urun)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("/api/yiyecekveicecek/ekle", urun);
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<YiyecekVeIcecekListeDTO>> YiyecekVeIcecekUrunGetirAsync()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<YiyecekVeIcecekListeDTO>>("/api/yiyecekveicecek/liste");
                return response ?? new List<YiyecekVeIcecekListeDTO>();
            }
            catch
            {
                return new List<YiyecekVeIcecekListeDTO>();
            }
        }


        public async Task<bool> YiyecekVeIcecekUrunGuncelleAsync(YiyecekVeIcecekGuncelleDTO guncel)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync("/api/yiyecekveicecek/guncelle", guncel);
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }


        public async Task<bool> YiyecekVeIcecekUrunSilAsync(Guid id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"/api/yiyecekveicecek/sil/{id}");
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        public async Task<(bool Basarili, string Mesaj)> YiyecekVeIcecekPUrunGuncelleAsync(Guid id, YiyecekVeIcecekDTO dto)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/yiyecekveicecek/guncelle/{id}", dto);
            return (response.IsSuccessStatusCode, response.IsSuccessStatusCode ? "Güncellendi" : await response.Content.ReadAsStringAsync());
        }

        
    }
}