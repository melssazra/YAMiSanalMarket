using Microsoft.Maui.Controls;
using YAMiMAUI.Services;
using YAMiMAUI.Baglanti;
using YAMiMAUI.ViewModels; // SepetimViewModel'e erişim için
using YAMiMAUI.Validation; // SepetimValidator'a erişim için

namespace YAMiMAUI.Kategoriler
{
    // ContentPage'den türetilir ve sadece View görevini üstlenir.
    public partial class Sepetim : ContentPage
    {
        private readonly SepetimViewModel _viewModel;

        // DI ile gerekli bağımlılıkları alır.
        public Sepetim(
            SepetServis sepetServis,
            SepetBaglanti sepetBaglanti,
            SiparisHandler siparisHandler,
            SepetimValidator validator)
        {
            InitializeComponent();

            // ViewModel'in oluşturulması ve atanması (BindingContext)
            _viewModel = new SepetimViewModel(sepetServis, sepetBaglanti, siparisHandler, validator);
            this.BindingContext = _viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // Sayfa yüklendiğinde ViewModel'deki verileri çekme metodu çağrılır.
            await _viewModel.YukleAsync(); // Artık bu metot ViewModel içinde tanımlı.
        }

        // Sayfadan çıkıldığında (View kaybolduğunda), ViewModel'deki kaynakları serbest bırakmak için 
        // IDisposable implementasyonu varsa Dispose çağrılmalıdır.
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            _viewModel.Dispose();
        }

        // NOT: XAML'den Clicked olayları (OnSepetiTemizleClicked, OdemeyiTamamlaClicked_Clicked) 
        // kaldırıldığı için, bu metotlar artık bu dosyada bulunmamaktadır. 
        // Tüm iş mantığı SepetimViewModel içinde Commands aracılığıyla yürütülür.
    }
}