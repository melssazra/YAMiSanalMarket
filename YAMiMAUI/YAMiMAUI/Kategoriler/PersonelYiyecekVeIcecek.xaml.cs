using System.Collections.ObjectModel;
using YAMiMAUI.Baglanti;
using YAMiMAUI.DTO;


namespace YAMiMAUI.Kategoriler
{
    public partial class PersonelYiyecekVeIcecek : ContentPage
    {
        public ObservableCollection<YiyecekVeIcecekListeDTO> urunListesi { get; set; } = new ObservableCollection<YiyecekVeIcecekListeDTO>();


        private readonly YiyecekVeIcecekBaglanti baglanti = new();

        public PersonelYiyecekVeIcecek()
        {
            InitializeComponent();
            BindingContext = this;
            Yukle();
        }

        // Ürünleri listeleme
        private async void Yukle()
        {
            var urunler = await baglanti.YiyecekVeIcecekUrunGetirAsync();
            urunListesi.Clear();
            foreach (var urun in urunler)
            {
                urunListesi.Add(urun);
            }
        }

        private YiyecekVeIcecekGuncelleDTO guncellenecekUrun;

        //ürünleri güncelle
        private void GuncelleButonClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var urun = button?.CommandParameter as YiyecekVeIcecekListeDTO;

            if (urun != null)
            {
                guncellenecekUrun = new YiyecekVeIcecekGuncelleDTO
                {
                    YId = urun.YId,
                    YAdi = urun.YAdi,
                    YSatisF = urun.YSatisF,
                    YAlis = urun.YAlisF, 
                    YMiktari = urun.YMiktari,
                    EkleyenPersonel = urun.EkleyenPersonel,
                    EklenmeTarihi = urun.EklenmeTarihi
                };

                // Entry'lere deðerleri doldur
                urunYeniAdiEntry.Text = guncellenecekUrun.YAdi;
                urunYeniSatisFEntry.Text = guncellenecekUrun.YSatisF?.ToString("F2") ?? "";
                urunYeniAlisEntry.Text = guncellenecekUrun.YAlis?.ToString("F2") ?? "";
                urunYeniMiktarEntry.Text = guncellenecekUrun.YMiktari?.ToString() ?? "";

                popupGuncelleKatmani.IsVisible = true;
            }
        }


        private async void PopupKaydetClicked(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(urunAdiEntry.Text) ||
                    string.IsNullOrWhiteSpace(urunSatisFEntry.Text) ||
                    string.IsNullOrWhiteSpace(urunAlisEntry.Text) ||
                    string.IsNullOrWhiteSpace(urunMiktarEntry.Text))
                {
                    await DisplayAlert("Hata", "Lütfen tüm alanlarý doldurun.", "Tamam");
                    return;
                }

                string ad = urunAdiEntry.Text;
                decimal satisF = decimal.Parse(urunSatisFEntry.Text);
                decimal alisF = decimal.Parse(urunAlisEntry.Text);
                int miktar = int.Parse(urunMiktarEntry.Text);
                string EkleyenPersonel = App.GirisYapanPersonelAdSoyad;
                DateTime EklenmeTarihi = DateTime.Now;

                var yeniUrun = new YiyecekVeIcecekEkleDTO
                {
                    YAdi = ad,
                    YSatisF = satisF,
                    YAlis = alisF,
                    YMiktari = miktar,
                    EkleyenPersonel = EkleyenPersonel ?? "Bilinmeyen Kullanýcý  ",
                    EklenmeTarihi = EklenmeTarihi
                };

                var baglanti = new YiyecekVeIcecekBaglanti();
                var sonuc = await baglanti.YiyecekVeIcecekUrunEkleAsync(yeniUrun);

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
                    string.IsNullOrWhiteSpace(urunYeniAlisEntry.Text) ||
                    string.IsNullOrWhiteSpace(urunYeniMiktarEntry.Text))
                {
                    await DisplayAlert("Hata", "Lütfen tüm alanlarý doldurun.", "Tamam");
                    return;
                }

                string ad = urunYeniAdiEntry.Text;
                decimal satisF = decimal.Parse(urunYeniSatisFEntry.Text);
                decimal alisF = decimal.Parse(urunYeniAlisEntry.Text);
                int miktar = int.Parse(urunYeniMiktarEntry.Text);

                var yeniUrun = new YiyecekVeIcecekDTO
                {
                    YId = guncellenecekUrun.YId,
                    YAdi = ad,
                    YSatisF = satisF,
                    YAlis = alisF,
                    YMiktari = miktar,
                    EkleyenPersonel = guncellenecekUrun.EkleyenPersonel,
                    EklenmeTarihi = guncellenecekUrun.EklenmeTarihi,
                    GuncelleyenPersonel = App.GirisYapanPersonelAdSoyad ?? "Bilinmeyen Kullanýcý",
                    GuncellemeTarihi = DateTime.Now
                };

                var baglanti = new YiyecekVeIcecekBaglanti();
                var sonuc = await baglanti.YiyecekVeIcecekPUrunGuncelleAsync(yeniUrun.YId, yeniUrun);

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
            var silinecek = button?.CommandParameter as YiyecekVeIcecekListeDTO;

            if (silinecek != null)
            {
                bool onay = await DisplayAlert("Sil", $"{silinecek.YAdi} silinsin mi?", "Evet", "Hayýr");
                if (onay)
                {
                    var sonuc = await baglanti.YiyecekVeIcecekUrunSilAsync(silinecek.YId);
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
        private void YiyecekVeIcecekUrunEkleClicked(object sender, EventArgs e)
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