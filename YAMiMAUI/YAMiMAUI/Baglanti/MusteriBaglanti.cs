using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using YAMiMAUI.DTO;
using Newtonsoft.Json;

namespace YAMiMAUI.Baglanti
{
    public class MusteriBaglanti : APIBaglanti
    {
        public async Task<HttpResponseMessage> MusteriGirisYapAsync(MusteriGirisDTO giris)
        {
            return await _httpClient.PostAsJsonAsync("/api/musteri/giris", giris);
        }



        public async Task<(bool Success, string Message)> MusteriKayitAsync(UyeOlDTO uye)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("/api/musteri/kayit", uye);
                if (response.IsSuccessStatusCode)
                {
                    return (true, "Kayıt başarılı");
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
                {
                    return (false, "Bu kullanıcı adı veya e-posta ile zaten bir üyelik mevcut.");
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    return (false, $"Kayıt sırasında bir hata oluştu: {error}");
                }
            }
            catch (Exception ex)
            {
                return (false, $"Hata: {ex.Message}");
            }
        }



        public async Task<bool> BaglantiTestiYapAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("/api/musteri");
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }



        public async Task<List<MusteriListeleDTO>> MusterileriGetirAsync()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<MusteriListeleDTO>>("/api/musteri/liste");
                return response ?? new List<MusteriListeleDTO>();
            }
            catch
            {
                return new List<MusteriListeleDTO>();
            }
        }


        public async Task<MusteriGuncelleDTO?> MusteriBilgiGetirAsync(Guid musteriId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/api/musteri/bilgi/{musteriId}");
                if (response.IsSuccessStatusCode)
                {
                    var musteri = await response.Content.ReadFromJsonAsync<MusteriGuncelleDTO>();
                    return musteri;
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }




        public async Task<HttpResponseMessage> MusteriGuncelleAsync(MusteriGuncelleDTO guncelleDTO)
        {
            return await _httpClient.PutAsJsonAsync("/api/musteri", guncelleDTO);
        }

        public async Task<bool> MusteriSilAsync(Guid musteriId)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"https://localhost:7170/api/musteri/sil/{musteriId}");
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }




        public async Task<HttpResponseMessage> SifremiUnuttumAsync(SifremiUnuttumDTO dto)
        {
            using (var client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(dto);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                return await client.PostAsync("https://localhost:7170/api/musteri/sifremiunuttum", content);
            }
        }

    }
}