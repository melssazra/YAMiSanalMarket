using System;
using System.Net.Http;
using System.Threading.Tasks;
using YAMiMAUI.Baglanti;
using YAMiMAUI.DTO;
using YAMiMAUI.Siniflar;

namespace YAMiMAUI.GirisEkrani
{
    public partial class PersonelGiris : ContentPage
    {
        private readonly PersonelBaglanti _personelBaglanti;

        public PersonelGiris()
        {
            try
            {
                InitializeComponent();
                _personelBaglanti = new PersonelBaglanti();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Hata: {ex.Message}");
            }
        }

        private async void PersonelGirisClicked(object sender, EventArgs e)
        {
            string username = UsernamePersonelEntry.Text;
            string password = PasswordPersonelEntry.Text;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                await DisplayAlert("Hata", "Kullanýcý adý ve þifre boþ olamaz", "Tamam");
                return;
            }

            try
            {
                var loginDto = new PersonelGirisDTO
                {
                    PKullaniciAdi = username,
                    PSifre = password
                };

                var response = await _personelBaglanti.LoginAsync(loginDto);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    //// JSON'u ekranda gösterme (deðiþken ismi için)
                    //await DisplayAlert("API JSON Cevap", json, "Tamam");
                    var gelenVeri = Newtonsoft.Json.JsonConvert.DeserializeObject<Personel>(json);    //giriþ yapan personelin bilgileri bu satýrla çekiliyor

                    App.GirisYapanPersonelAdSoyad = gelenVeri.PAdSoyad;

                    await DisplayAlert("Bilgi", "Giriþ yapan: " + App.GirisYapanPersonelAdSoyad, "Tamam");

                    await Navigation.PushAsync(new KategoriSecme.PersonelKategori());
                }
                else
                {
                    string errorMessage = await response.Content.ReadAsStringAsync();
                    await DisplayAlert("Hata", $"Kullanýcý adý veya þifre hatalý: {errorMessage}", "Tamam");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Giriþ hatasý: {ex.Message}");
                await DisplayAlert("Hata", "Giriþ sýrasýnda bir hata oluþtu", "Tamam");
            }
        }
    }
}