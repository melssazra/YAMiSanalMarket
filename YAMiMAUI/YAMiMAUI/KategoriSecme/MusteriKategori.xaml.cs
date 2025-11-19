using System.Collections.ObjectModel;
using YAMiMAUI.DTO;
using YAMiMAUI.Services;
using YAMiMAUI.Baglanti; // MusteriBaglanti için
using YAMiMAUI.Siniflar; // Oturum sýnýfý için


namespace YAMiMAUI.KategoriSecme;

public partial class MusteriKategori : ContentPage
{
    private readonly SepetServis _sepetServis;
    private readonly IServiceProvider _serviceProvider;
    private readonly MusteriBaglanti _musteriBaglanti; // MusteriBaglanti eklendi

    // Constructor güncellendi: SepetServis, IServiceProvider VE MusteriBaglanti alacak
    public MusteriKategori(SepetServis sepetServis, IServiceProvider serviceProvider, MusteriBaglanti musteriBaglanti)
    {
        InitializeComponent();
        _sepetServis = sepetServis;
        _serviceProvider = serviceProvider;
        _musteriBaglanti = musteriBaglanti; // Atandý
    }

    // Bilgilerim butonu týklama olayý
    private async void BilgilerimClicked(object sender, EventArgs e)
    {
        // Sayfa DI ile alýnýyor
        await Navigation.PushAsync(_serviceProvider.GetService<Kategoriler.Bilgilerim>());
    }

    // Yiyecek ve Ýçecek kategorisi butonu týklama olayý
    private async void YiyecekVeIcecekClicked(object sender, EventArgs e)
    {
        // Sayfa DI ile alýnýyor
        await Navigation.PushAsync(_serviceProvider.GetService<Kategoriler.MusteriYiyecekVeIcecek>());
    }

    // Meyve ve Sebze kategorisi butonu týklama olayý
    private async void MeyveVeSebzeClicked(object sender, EventArgs e)
    {
        // Sayfa DI ile alýnýyor
        await Navigation.PushAsync(_serviceProvider.GetService<Kategoriler.MusteriMeyveVeSebze>());
    }

    // Kozmetik kategorisi butonu týklama olayý
    private async void KozmetikClicked(object sender, EventArgs e)
    {
        // Sayfa DI ile alýnýyor
        await Navigation.PushAsync(_serviceProvider.GetService<Kategoriler.MusteriKozmetik>());
    }

    // Pastane kategorisi butonu týklama olayý
    private async void PastaneClicked(object sender, EventArgs e)
    {
        // Sayfa DI ile alýnýyor
        await Navigation.PushAsync(_serviceProvider.GetService<Kategoriler.MusteriPastane>());
    }

   

    // Sepetim butonu týklama olayý
    private async void SepetimClicked(object sender, EventArgs e)
    {
        Guid musteriId = Oturum.MusteriId; // Oturum sýnýfýnýzdan MusteriId'yi alýn

        if (musteriId == Guid.Empty)
        {
            await DisplayAlert("Hata", "Müþteri bilgisi alýnamadý. Lütfen tekrar giriþ yapýn.", "Tamam");
            return;
        }

        // Sepetim'e geçmeden önce güncel sepeti API'den yükle
        await _sepetServis.SepetiYukleAsync(musteriId);
        // Sepetim sayfasý artýk SepetServis baðýmlýlýðýna sahip, bu yüzden DI ile alýyoruz.
        await Navigation.PushAsync(_serviceProvider.GetService<Kategoriler.Sepetim>());
    }
}