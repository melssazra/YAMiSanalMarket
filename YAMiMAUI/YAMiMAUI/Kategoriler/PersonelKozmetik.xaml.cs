using System.Collections.ObjectModel;
using YAMiMAUI.Baglanti;
using YAMiMAUI.DTO;

namespace YAMiMAUI.Kategoriler;

public partial class PersonelKozmetik : ContentPage
{
    public ObservableCollection<KozmetikListeDTO> urunListesi { get; set; } = new ObservableCollection<KozmetikListeDTO>();


    private readonly KozmetikBaglanti baglanti = new();

    public PersonelKozmetik()
    {
        InitializeComponent();
        BindingContext = this;
        Yukle();
    }

    // Ürünleri listeleme
    private async void Yukle()
    {
        var urunler = await baglanti.KozmetikUrunGetirAsync();
        urunListesi.Clear();
        foreach (var urun in urunler)
        {
            urunListesi.Add(urun);
        }
    }

    private KozmetikGuncelleDTO guncellenecekUrun;

    //ürünleri güncelle
    private void GuncelleButonClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var urun = button?.CommandParameter as KozmetikListeDTO;

        if (urun != null)
        {
            
            guncellenecekUrun = new KozmetikGuncelleDTO
            {
                KId = urun.KId,
                KAdi = urun.KAdi,
                KSatisF = urun.KSatisF,
                KAlis = urun.KAlisF, 
                KMiktari = urun.KMiktari,
                EkleyenPersonel = urun.EkleyenPersonel,
                EklenmeTarihi = urun.EklenmeTarihi
            };

            // Entry'lere deðerleri doldur
            urunYeniAdiEntry.Text = guncellenecekUrun.KAdi;
            urunYeniSatisFEntry.Text = guncellenecekUrun.KSatisF?.ToString("F2") ?? "";
            urunYeniAlisFEntry.Text = guncellenecekUrun.KAlis?.ToString("F2") ?? "";
            urunYeniMiktarEntry.Text = guncellenecekUrun.KMiktari?.ToString() ?? "";

            popupGuncelleKatmani.IsVisible = true;
        }
    }


    private async void PopupKaydetClicked(object sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(urunAdiEntry.Text) ||
                string.IsNullOrWhiteSpace(urunSatisFEntry.Text) ||
                string.IsNullOrWhiteSpace(urunAlisFEntry.Text) ||
                string.IsNullOrWhiteSpace(urunMiktarEntry.Text))
            {
                await DisplayAlert("Hata", "Lütfen tüm alanlarý doldurun.", "Tamam");
                return;
            }

            string ad = urunAdiEntry.Text;
            decimal satisF = decimal.Parse(urunSatisFEntry.Text);
            decimal alisF = decimal.Parse(urunAlisFEntry.Text);
            int miktar = int.Parse(urunMiktarEntry.Text);
            string EkleyenPersonel = App.GirisYapanPersonelAdSoyad;
            DateTime EklenmeTarihi = DateTime.Now;

            var yeniUrun = new KozmetikEkleDTO
            {
                KAdi = ad,
                KSatisF = satisF,
                KAlis = alisF,
                KMiktari = miktar,
                EkleyenPersonel = EkleyenPersonel ?? "Bilinmeyen Kullanýcý  ",
                EklenmeTarihi = EklenmeTarihi
            };

            var baglanti = new KozmetikBaglanti();
            var sonuc = await baglanti.KozmetikUrunEkleAsync(yeniUrun);

            if (sonuc)
            {
                await DisplayAlert("Baþarýlý", "Ürün eklendi", "Tamam");
                popupKatmani.IsVisible = false;
            }
            else
            {
                await DisplayAlert("Hata", "Ekleme baþarýsýz\n" + sonuc, "Tamam");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Hata", $"Beklenmeyen hata oluþtu:\n{ex.Message}", "Tamam");
        }
    }

    private void PopupGIptalClicked(object sender, EventArgs e)
    {
        popupGuncelleKatmani.IsVisible = false;
    }

    private async void PopupGuncelleClicked(object sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(urunYeniAdiEntry.Text) ||
                string.IsNullOrWhiteSpace(urunYeniSatisFEntry.Text) ||
                string.IsNullOrWhiteSpace(urunYeniAlisFEntry.Text) ||
                string.IsNullOrWhiteSpace(urunYeniMiktarEntry.Text))
            {
                await DisplayAlert("Hata", "Lütfen tüm alanlarý doldurun.", "Tamam");
                return;
            }

            string ad = urunYeniAdiEntry.Text;
            decimal satisF = decimal.Parse(urunYeniSatisFEntry.Text);
            decimal alisF = decimal.Parse(urunYeniAlisFEntry.Text);
            int miktar = int.Parse(urunYeniMiktarEntry.Text);

            var yeniUrun = new KozmetikDTO
            {
                KId = guncellenecekUrun.KId,
                KAdi = ad,
                KSatisF = satisF,
                KAlis = alisF,
                KMiktari = miktar,
                EkleyenPersonel = guncellenecekUrun.EkleyenPersonel,
                EklenmeTarihi = guncellenecekUrun.EklenmeTarihi,
                GuncelleyenPersonel = App.GirisYapanPersonelAdSoyad ?? "Bilinmeyen Kullanýcý",
                GuncellemeTarihi = DateTime.Now
            };

            var baglanti = new KozmetikBaglanti();
            var sonuc = await baglanti.KozmetikKUrunGuncelleAsync(yeniUrun.KId, yeniUrun);

            if (sonuc.Basarili)
            {
                await DisplayAlert("Baþarýlý", "Ürün güncellendi", "Tamam");
                popupGuncelleKatmani.IsVisible = false;
                Yukle();
            }
            else
            {
                await DisplayAlert("Hata", "Güncelleme baþarýsýz\n" + sonuc.Mesaj, "Tamam");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Hata", $"Beklenmeyen hata oluþtu:\n{ex.Message}", "Tamam");
        }
    }



    // Ürünleri silme
    private async void SilButonClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var silinecek = button?.CommandParameter as KozmetikListeDTO;

        if (silinecek != null)
        {
            bool onay = await DisplayAlert("Sil", $"{silinecek.KAdi} silinsin mi?", "Evet", "Hayýr");
            if (onay)
            {
                var sonuc = await baglanti.KozmetikUrunSilAsync(silinecek.KId);
                if (sonuc)
                {
                    urunListesi.Remove(silinecek);
                    await DisplayAlert("Baþarýlý", "Ürün silindi", "Tamam");
                }
                else
                {
                    await DisplayAlert("Hata", "Silme baþarýsýz", "Tamam");
                }
            }
        }
    }



    // Ürün ekleme
    private void PastaneUrunEkleClicked(object sender, EventArgs e)
    {
        urunAdiEntry.Text = "";
        urunSatisFEntry.Text = "";
        urunMiktarEntry.Text = "";
        popupKatmani.IsVisible = true;
    }


    private void PopupIptalClicked(object sender, EventArgs e)
    {
        popupKatmani.IsVisible = false;
    }
}