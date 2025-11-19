using System.Collections.ObjectModel;
using YAMiMAUI.Baglanti;
using YAMiMAUI.DTO;


namespace YAMiMAUI.Kategoriler
{
    public partial class PersonelMeyveVeSebze : ContentPage
    {
        public ObservableCollection<MeyveVeSebzeListeDTO> urunListesi { get; set; } = new ObservableCollection<MeyveVeSebzeListeDTO>();


        private readonly MeyveVeSebzeBaglanti baglanti = new();

        public PersonelMeyveVeSebze()
        {
            InitializeComponent();
            BindingContext = this;
            Yukle();
        }

        // Ürünleri listeleme
        private async void Yukle()
        {
            var urunler = await baglanti.MeyveVeSebzeUrunGetirAsync();
            urunListesi.Clear();
            foreach (var urun in urunler)
            {
                urunListesi.Add(urun);
            }
        }

        private MeyveVeSebzeGuncelleDTO guncellenecekUrun;

        //ürünleri güncelle
        private void GuncelleButonClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var urun = button?.CommandParameter as MeyveVeSebzeListeDTO;

            if (urun != null)
            {
                
                guncellenecekUrun = new MeyveVeSebzeGuncelleDTO
                {
                    MId = urun.MId,
                    MAdi = urun.MAdi,
                    MSatisF = urun.MSatisF,
                    MAlis = urun.MAlisF,
                    MMiktari = urun.MMiktari,
                    EkleyenPersonel = urun.EkleyenPersonel,
                    EklenmeTarihi = urun.EklenmeTarihi
                };

                // Entry'lere deðerleri doldur
                urunYeniAdiEntry.Text = guncellenecekUrun.MAdi;
                urunYeniSatisFEntry.Text = guncellenecekUrun.MSatisF?.ToString("F2") ?? "";
                urunYeniAlisFEntry.Text = guncellenecekUrun.MAlis?.ToString("F2") ?? "";
                urunYeniMiktarEntry.Text = guncellenecekUrun.MMiktari?.ToString() ?? "";

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

                var yeniUrun = new MeyveVeSebzeEkleDTO
                {
                    MAdi = ad,
                    MSatisF = satisF,
                    MAlis = alisF,
                    MMiktari = miktar,
                    EkleyenPersonel = EkleyenPersonel ?? "Bilinmeyen Kullanýcý  ",
                    EklenmeTarihi = EklenmeTarihi
                };

                var baglanti = new MeyveVeSebzeBaglanti();
                var sonuc = await baglanti.MeyveVeSebzeUrunEkleAsync(yeniUrun);

                if (sonuc)
                {
                    await DisplayAlert("Baþarýlý", "Ürün eklendi", "Tamam");
                    Yukle();
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

                var yeniUrun = new MeyveVeSebzeDTO
                {
                    MId = guncellenecekUrun.MId,
                    MAdi = ad,
                    MSatisF = satisF,
                    MAlis = alisF,
                    MMiktari = miktar,
                    EkleyenPersonel = guncellenecekUrun.EkleyenPersonel,
                    EklenmeTarihi = guncellenecekUrun.EklenmeTarihi,
                    GuncelleyenPersonel = App.GirisYapanPersonelAdSoyad ?? "Bilinmeyen Kullanýcý",
                    GuncellemeTarihi = DateTime.Now
                };

                var baglanti = new MeyveVeSebzeBaglanti();
                var sonuc = await baglanti.MeyveVeSebzePUrunGuncelleAsync(yeniUrun.MId, yeniUrun);

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
            var silinecek = button?.CommandParameter as MeyveVeSebzeListeDTO;

            if (silinecek != null)
            {
                bool onay = await DisplayAlert("Sil", $"{silinecek.MAdi} silinsin mi?", "Evet", "Hayýr");
                if (onay)
                {
                    var sonuc = await baglanti.MeyveVeSebzeUrunSilAsync(silinecek.MId);
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
        private void MeyveVeSebzeUrunEkleClicked(object sender, EventArgs e)
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
}