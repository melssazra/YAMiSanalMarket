using YAMiMAUI.Baglanti;
using YAMiMAUI.GirisEkrani; // MusteriGiris için ekleyin
using Microsoft.Extensions.DependencyInjection; // IServiceProvider için ekleyin

namespace YAMiMAUI
{
    public partial class MainPage : ContentPage
    {
        private readonly IServiceProvider _serviceProvider; // IServiceProvider alanı eklendi

        // Constructor'ı güncelleyin: IServiceProvider parametresi alacak
        public MainPage(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider; // IServiceProvider atandı
        }

        private async void PersonelGirisClicked(object sender, EventArgs e)
        {
            // Eğer PersonelGiris sayfası da DI ile bağımlılık alıyorsa:
            // await Navigation.PushAsync(_serviceProvider.GetService<GirisEkrani.PersonelGiris>());
            await Navigation.PushAsync(new GirisEkrani.PersonelGiris()); // Mevcut çağrıya göre bırakıldı
        }

        private async void MusteriGirisiClicked(object sender, EventArgs e)
        {
            // MusteriGiris sayfasını DI ile çözümleyerek çağırın
            await Navigation.PushAsync(_serviceProvider.GetService<MusteriGiris>());
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await Task.Delay(200);

            // MusteriBaglanti'yı da DI üzerinden alın
            var baglanti = _serviceProvider.GetService<MusteriBaglanti>();
            //if (baglanti != null) // null kontrolü eklendi
            //{
            //    bool sonuc = await baglanti.BaglantiTestiYapAsync();

            //    if (sonuc)
            //        await DisplayAlert("Durum", "API bağlantısı başarılı!", "Tamam");
            //    else
            //        await DisplayAlert("Durum", "API bağlantısı başarısız.", "Tamam");
            //}
            //else
            //{
            //    await DisplayAlert("Hata", "MusteriBaglanti servisi çözümlenemedi.", "Tamam");
            //}
        }
    }
}