using System.Net.Http.Json;
using YAMiMAUI.Baglanti;
using YAMiMAUI.DTO;
using YAMiMAUI.Siniflar;
using Microsoft.Extensions.DependencyInjection; // IServiceProvider için eklendi

namespace YAMiMAUI.Kategoriler
{
    public partial class Bilgilerim : ContentPage
    {
        // MusteriBaglanti'yý DI ile alacaðýz, doðrudan oluþturmayacaðýz.
        private readonly MusteriBaglanti _musteriBaglanti;
        private readonly IServiceProvider _serviceProvider; // Gerekiyorsa diðer servisleri almak için

        // Constructor'ý güncelledik: MusteriBaglanti ve IServiceProvider alacak
        public Bilgilerim(MusteriBaglanti musteriBaglanti, IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _musteriBaglanti = musteriBaglanti;
            _serviceProvider = serviceProvider;
           
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            try
            {
                var musteri = await _musteriBaglanti.MusteriBilgiGetirAsync(Oturum.MusteriId);

                if (musteri != null)
                {
                    usernameEntry.Text = musteri.MKullaniciAdi;
                    passwordEntry.Text = musteri.MSifre;
                    nameEntry.Text = musteri.MAdSoyad;
                    mailEntry.Text = musteri.MEmail;
                    phoneEntry.Text = musteri.MTelNo;
                    addressEntry.Text = musteri.MAdres;
                }
                else
                {
                    await DisplayAlert("Hata", "Kullanýcý bilgileri alýnamadý.", "Tamam");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Hata", $"Beklenmedik hata: {ex.Message}", "Tamam");
            }
        }


        private async void GuncelleClicked(object sender, EventArgs e)
        {
            var guncelleDTO = new MusteriGuncelleDTO
            {
                MusteriId = Oturum.MusteriId,
                MKullaniciAdi = usernameEntry.Text,
                MSifre = passwordEntry.Text,
                MAdSoyad = nameEntry.Text,
                MEmail = mailEntry.Text,
                MTelNo = phoneEntry.Text,
                MAdres = addressEntry.Text
            };

            var response = await _musteriBaglanti.MusteriGuncelleAsync(guncelleDTO);

            if (response.IsSuccessStatusCode)
            {
                await DisplayAlert("Baþarýlý", "Bilgiler güncellendi.", "Tamam");
            }
            else
            {
                await DisplayAlert("Hata", "Güncelleme baþarýsýz.", "Tamam");
            }
        }

        private async void HesapSilClicked(object sender, EventArgs e)
        {
            bool onay = await DisplayAlert("Hesap Sil", "Hesabýnýzý silmek istediðinizden emin misiniz?", "Evet", "Hayýr");

            if (onay)
            {
                var sonuc = await _musteriBaglanti.MusteriSilAsync(Oturum.MusteriId);

                if (sonuc)
                {
                    await DisplayAlert("Baþarýlý", "Hesabýnýz silindi.", "Tamam");
                    Oturum.MusteriId = Guid.Empty;
                    // Ana sayfaya dönmek için DI üzerinden MainPage'i çözün
                    Application.Current.MainPage = new NavigationPage(_serviceProvider.GetService<MainPage>());
                }
                else
                {
                    await DisplayAlert("Hata", "Hesap silinemedi.", "Tamam");
                }
            }
        }


        private async void OturumKapatClicked(object sender, EventArgs e)
        {
            bool cikis = await DisplayAlert("Çýkýþ", "Oturumu kapatmak istiyor musunuz?", "Evet", "Hayýr");

            if (cikis)
            {
                Oturum.MusteriId = Guid.Empty;
                // Ana sayfaya dönmek için DI üzerinden MainPage'i çözün
                Application.Current.MainPage = new NavigationPage(_serviceProvider.GetService<MainPage>());
            }
        }
    }
}