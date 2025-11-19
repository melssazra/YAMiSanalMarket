using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using YAMiMAUI.DTO;

namespace YAMiMAUI.Baglanti
{
    public class PersonelBaglanti : APIBaglanti
    {
        public async Task<HttpResponseMessage> LoginAsync(PersonelGirisDTO personelGirisDTO)
        {
            var response = await _httpClient.PostAsJsonAsync("api/personel/giris", personelGirisDTO);
            if (response.IsSuccessStatusCode)
            {
                var personel = await response.Content.ReadFromJsonAsync<PersonelGirisDTO>(); // PersonelDTO, sunucudan dönen personel bilgilerini içermeli
                App.GirisYapanPersonelAdSoyad = personel?.PKullaniciAdi; // Örnek bir alan
            }
            return response;
        }
    }
}