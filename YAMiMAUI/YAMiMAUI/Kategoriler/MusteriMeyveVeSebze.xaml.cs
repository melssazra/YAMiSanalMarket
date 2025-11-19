using System.Collections.ObjectModel;
using System.Windows.Input;
using YAMiMAUI.Baglanti;
using YAMiMAUI.DTO;
using YAMiMAUI.Services;
using YAMiMAUI.Siniflar; // Oturum sýnýfý için
using Microsoft.Extensions.DependencyInjection; // IServiceProvider için

namespace YAMiMAUI.Kategoriler
{
    public partial class MusteriMeyveVeSebze : ContentPage
    {
        private readonly MeyveVeSebzeBaglanti _meyveVeSebzeBaglanti;
        private readonly SepetServis _sepetServis;
        private readonly IServiceProvider _serviceProvider;

        public ObservableCollection<MeyveVeSebzeListeDTO> Urunler { get; set; }
        public ICommand SepeteEkleCommand { get; set; }

        public MusteriMeyveVeSebze(MeyveVeSebzeBaglanti meyveVeSebzeBaglanti, SepetServis sepetServis, IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _meyveVeSebzeBaglanti = meyveVeSebzeBaglanti;
            _sepetServis = sepetServis;
            _serviceProvider = serviceProvider;
            Urunler = new ObservableCollection<MeyveVeSebzeListeDTO>();
            SepeteEkleCommand = new Command<Guid>(async (id) => await SepeteEkleAsync(id));
            BindingContext = this;
            YukleUrunler();
        }

        private async void YukleUrunler()
        {
            try
            {
                var urunler = await _meyveVeSebzeBaglanti.MeyveVeSebzeUrunGetirAsync();
                if (urunler != null)
                {
                    Urunler.Clear();
                    foreach (var urun in urunler)
                    {
                        Urunler.Add(urun);
                    }
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Hata", "Ürünler yüklenirken bir hata oluþtu: " + ex.Message, "Tamam");
            }
        }

        private async Task SepeteEkleAsync(Guid urunId)
        {
            try
            {
                var urun = Urunler.FirstOrDefault(u => u.MId == urunId);
                if (urun == null || urun.MMiktari <= 0)
                {
                    await DisplayAlert("Uyarý", "Bu ürün stokta yok veya geçersiz.", "Tamam");
                    return;
                }

                string adetStr = await DisplayPromptAsync("Kg Girin", $"Kaç Kg {urun.MAdi} eklemek istersiniz? (Mevcut Stok: {urun.MMiktari})", "Ekle", "Ýptal", keyboard: Keyboard.Numeric);

                if (adetStr == null)
                {
                    return;
                }

                if (!int.TryParse(adetStr, out int eklenenAdet) || eklenenAdet <= 0)
                {
                    await DisplayAlert("Uyarý", "Geçerli bir kg girmelisiniz.", "Tamam");
                    return;
                }

                if (eklenenAdet > urun.MMiktari)
                {
                    await DisplayAlert("Uyarý", $"Yeterli stok yok. Maksimum {urun.MMiktari} kg ekleyebilirsiniz.", "Tamam");
                    return;
                }

                Guid musteriId = Oturum.MusteriId;
                if (musteriId == Guid.Empty)
                {
                    await DisplayAlert("Hata", "Müþteri bilgisi alýnamadý. Lütfen tekrar giriþ yapýn.", "Tamam");
                    return;
                }

                // Sepet servisi aracýlýðýyla ürünü sepete ekle (API'ye istek gidecek)
                await _sepetServis.SepeteUrunEkleAsync(musteriId, urun.MId, "MeyveVeSebze", eklenenAdet);

                // Stoktan düþ ve API'ye güncelleme gönder
                urun.MMiktari -= eklenenAdet;
                await _meyveVeSebzeBaglanti.MeyveVeSebzeUrunGuncelleAsync(new MeyveVeSebzeGuncelleDTO
                {
                    MId = urun.MId,
                    MMiktari = urun.MMiktari
                });

                await DisplayAlert("Bilgi", $"{eklenenAdet} kg {urun.MAdi} sepete eklendi ve stok güncellendi.", "Tamam");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hata oluþtu: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                }
                await DisplayAlert("Hata", "Sepete ekleme sýrasýnda bir hata oluþtu: " + ex.Message, "Tamam");
            }
        }

        private async void SepetimClicked(object sender, EventArgs e)
        {
            Guid musteriId = Oturum.MusteriId; // Oturum sýnýfýndan MusteriId'yi alýn

            if (musteriId == Guid.Empty)
            {
                await DisplayAlert("Hata", "Müþteri bilgisi alýnamadý. Lütfen giriþ yapýn.", "Tamam");
                return;
            }

            // Sepetim'e geçmeden önce güncel sepeti API'den yükle
            await _sepetServis.SepetiYukleAsync(musteriId);

            // Sepetim sayfasý artýk SepetServis baðýmlýlýðýna sahip, bu yüzden DI ile alýyoruz.
            await Navigation.PushAsync(_serviceProvider.GetService<Kategoriler.Sepetim>());
        }
    }
}
