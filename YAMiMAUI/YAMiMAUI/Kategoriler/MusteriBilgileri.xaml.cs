using YAMiMAUI.Baglanti;
using YAMiMAUI.DTO;

namespace YAMiMAUI.Kategoriler
{
    public partial class MusteriBilgileri : ContentPage
    {
        private readonly MusteriBaglanti _baglanti = new();

        public MusteriBilgileri()
        {
            InitializeComponent();
            Yukle();
        }

        private async void Yukle()
        {
            var musteriler = await _baglanti.MusterileriGetirAsync();
            MusteriCollectionView.ItemsSource = musteriler;
        }
    }
}