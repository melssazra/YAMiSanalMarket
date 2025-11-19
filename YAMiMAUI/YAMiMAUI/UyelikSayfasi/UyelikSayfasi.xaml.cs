using System;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using YAMiMAUI.Baglanti;
using YAMiMAUI.DTO;
using YAMiMAUI.GirisEkrani; // MusteriGiris için
using Microsoft.Extensions.DependencyInjection; // IServiceProvider için

namespace YAMiMAUI.UyelikSayfasi
{
    public partial class UyelikSayfasi : ContentPage
    {
        private readonly MusteriBaglanti _musteriBaglanti;
        private readonly IServiceProvider _serviceProvider; // IServiceProvider alanı eklendi

        // Constructor'ı güncelleyin: MusteriBaglanti ve IServiceProvider parametreleri alacak
        public UyelikSayfasi(MusteriBaglanti musteriBaglanti, IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _musteriBaglanti = musteriBaglanti; // DI ile alınan MusteriBaglanti atandı
            _serviceProvider = serviceProvider; // DI ile alınan IServiceProvider atandı
        }

        private async void UyeOlClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(nameEntry.Text) || string.IsNullOrWhiteSpace(usernameEntry.Text) ||
                string.IsNullOrWhiteSpace(passwordEntry.Text) || string.IsNullOrWhiteSpace(mailEntry.Text) ||
                string.IsNullOrWhiteSpace(phoneEntry.Text) || string.IsNullOrWhiteSpace(addressEntry.Text))
            {
                await DisplayAlert("Hata", "Tüm alanları doldurunuz", "Tamam");
                return;
            }

            if (!Regex.IsMatch(phoneEntry.Text, @"^\d{11}$"))
            {
                await DisplayAlert("Hata", "Telefon numarası 11 haneli ve sadece rakamlardan oluşmalıdır.", "Tamam");
                return;
            }

            var uye = new UyeOlDTO
            {
                MAdSoyad = nameEntry.Text,
                MKullaniciAdi = usernameEntry.Text,
                MSifre = passwordEntry.Text,
                MEmail = mailEntry.Text,
                MTelNo = phoneEntry.Text,
                MAdres = addressEntry.Text
            };

            try
            {
                var (basarili, mesaj) = await _musteriBaglanti.MusteriKayitAsync(uye);

                if (basarili)
                {
                    await DisplayAlert("Başarılı", "Kayıt işlemi başarılı", "Tamam");
                    // MusteriGiris sayfasını DI ile çözümleyerek çağırın
                    await Navigation.PushAsync(_serviceProvider.GetService<MusteriGiris>());
                }
                else
                {
                    await DisplayAlert("Hata", mesaj, "Tamam");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Kayıt hatası: {ex.Message}");
                await DisplayAlert("Hata", $"Bir hata oluştu: {ex.Message}", "Tamam");
            }
        }
    }
}