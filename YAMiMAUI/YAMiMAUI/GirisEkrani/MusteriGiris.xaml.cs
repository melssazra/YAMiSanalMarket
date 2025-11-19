using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using YAMiMAUI.Baglanti;
using YAMiMAUI.DTO;
using YAMiMAUI.Siniflar; // MusteriGirisDTO için
using Microsoft.Extensions.DependencyInjection; // IServiceProvider için

namespace YAMiMAUI.GirisEkrani
{
    public partial class MusteriGiris : ContentPage
    {
        private readonly MusteriBaglanti _musteriBaglanti;
        private readonly IServiceProvider _serviceProvider; // IServiceProvider eklendi

        // Constructor güncellendi: MusteriBaglanti ve IServiceProvider alacak
        public MusteriGiris(MusteriBaglanti musteriBaglanti, IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _musteriBaglanti = musteriBaglanti; // DI ile alýnan MusteriBaglanti atandý
            _serviceProvider = serviceProvider; // DI ile alýnan IServiceProvider atandý
            this.BindingContext = this; // Eðer XAML'de binding kullanýlýyorsa
        }

        private async void GirisClicked(object sender, EventArgs e)
        {
            string username = usernameEntry.Text;
            string password = passwordEntry.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                await DisplayAlert("Hata", "Kullanýcý adý ve þifre boþ olamaz", "Tamam");
                return;
            }

            try
            {
                var loginDto = new MusteriGirisDTO
                {
                    MKullaniciAdi = username,
                    MSifre = password
                };

                // MusteriGirisYapAsync metodu MusteriBaglanti'da tanýmlý olmalý
                var response = await _musteriBaglanti.MusteriGirisYapAsync(loginDto);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<MusteriGirisResponseDTO>(json); // MusteriGirisResponseDTO kullanýn

                    Oturum.MusteriId = result.MusteriId; // DTO'dan MusteriId'yi alýn
                    Oturum.MKullaniciAdi = result.MKullaniciAdi; // DTO'dan Kullanýcý Adýný alýn

                    // MusteriKategori sayfasýný DI ile çözümleyerek çaðýrýn
                    await Navigation.PushAsync(_serviceProvider.GetService<KategoriSecme.MusteriKategori>());
                }
                else
                {
                    string hataMesaji = await response.Content.ReadAsStringAsync();
                    await DisplayAlert("Giriþ Baþarýsýz", hataMesaji, "Tamam");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Hata", $"Bir hata oluþtu: {ex.Message}", "Tamam");
            }
        }

        // Düzeltilen UyeOlClicked metodu
        private async void UyeOlClicked(object sender, EventArgs e)
        {
            // UyelikSayfasi'ný DI ile çözümleyerek çaðýrýn
            await Navigation.PushAsync(_serviceProvider.GetService<UyelikSayfasi.UyelikSayfasi>());
        }

        private async void SifremiUnuttumClicked(object sender, EventArgs e)
        {
            string kullaniciAdi = await DisplayPromptAsync("Þifremi Unuttum", "Kullanýcý Adýnýzý Giriniz:");
            if (string.IsNullOrWhiteSpace(kullaniciAdi)) return;

            string telefon = await DisplayPromptAsync("Þifremi Unuttum", "Telefon Numaranýzý Giriniz:");
            if (string.IsNullOrWhiteSpace(telefon)) return;

            try
            {
                var dto = new SifremiUnuttumDTO
                {
                    MKullaniciAdi = kullaniciAdi,
                    MTelNo = telefon
                };

                var sonuc = await _musteriBaglanti.SifremiUnuttumAsync(dto);

                if (sonuc.IsSuccessStatusCode)
                {
                    string mesaj = await sonuc.Content.ReadAsStringAsync();
                    await DisplayAlert("Baþarýlý", mesaj, "Tamam");
                }
                else
                {
                    string hata = await sonuc.Content.ReadAsStringAsync();
                    await DisplayAlert("Hata", hata, "Tamam");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Hata", $"Bir hata oluþtu: {ex.Message}", "Tamam");
            }
        }
    }
}