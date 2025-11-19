using System.Collections.ObjectModel;
using YAMiMAUI.Baglanti;
using YAMiMAUI.DTO;


namespace YAMiMAUI.Kategoriler
{
    public partial class PersonelPastane : ContentPage
    {
        public ObservableCollection<PastaneListeDTO> urunListesi { get; set; } = new ObservableCollection<PastaneListeDTO>();


        private readonly PastaneBaglanti baglanti = new();

        public PersonelPastane()
        {
            InitializeComponent();
            BindingContext = this;
            Yukle();
        }

        // Ürünleri listeleme
        private async void Yukle()
        {
            var urunler = await baglanti.PastaneUrunGetirAsync();
            urunListesi.Clear();
            foreach (var urun in urunler)
            {
                urunListesi.Add(urun);
            }
        }

        private PastaneGuncelleDTO guncellenecekUrun;

        //ürünleri güncelle
        private void GuncelleButonClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var urun = button?.CommandParameter as PastaneListeDTO; 

            if (urun != null)
            {
                // Güncelleme için PastaneGuncelleDTO oluþtur
                guncellenecekUrun = new PastaneGuncelleDTO
                {
                    PId = urun.PId,
                    PAdi = urun.PAdi,
                    PSatisF = urun.PSatisF,
                    PAlis = urun.PAlisF, 
                    PMiktari = urun.PMiktari,
                    EkleyenPersonel = urun.EkleyenPersonel, 
                    EklenmeTarihi = urun.EklenmeTarihi 
                };

                // Entry'lere deðerleri doldur
                urunYeniAdiEntry.Text = guncellenecekUrun.PAdi;
                urunYeniSatisFEntry.Text = guncellenecekUrun.PSatisF?.ToString("F2") ?? "";
                urunYeniAlisFEntry.Text = guncellenecekUrun.PAlis?.ToString("F2") ?? "";
                urunYeniMiktarEntry.Text = guncellenecekUrun.PMiktari?.ToString() ?? "";

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

                var yeniUrun = new PastaneEkleDTO
                {
                    PAdi = ad,
                    PSatisF = satisF,
                    PAlis = alisF,
                    PMiktari = miktar,
                    EkleyenPersonel = EkleyenPersonel ?? "Bilinmeyen Kullanýcý  ",
                    EklenmeTarihi = EklenmeTarihi
                };

                var baglanti = new PastaneBaglanti();
                var sonuc = await baglanti.PastaneUrunEkleAsync(yeniUrun);

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

                var yeniUrun = new PastaneDTO
                {
                    PId = guncellenecekUrun.PId,
                    PAdi = ad,
                    PSatisF = satisF,
                    PAlis = alisF,
                    PMiktari = miktar,
                    EkleyenPersonel = guncellenecekUrun.EkleyenPersonel,
                    EklenmeTarihi = guncellenecekUrun.EklenmeTarihi,
                    GuncelleyenPersonel = App.GirisYapanPersonelAdSoyad ?? "Bilinmeyen Kullanýcý",
                    GuncellemeTarihi = DateTime.Now
                };

                var baglanti = new PastaneBaglanti();
                var sonuc = await baglanti.PastanePUrunGuncelleAsync(yeniUrun.PId, yeniUrun);

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
            var silinecek = button?.CommandParameter as PastaneListeDTO;

            if (silinecek != null)
            {
                bool onay = await DisplayAlert("Sil", $"{silinecek.PAdi} silinsin mi?", "Evet", "Hayýr");
                if (onay)
                {
                    var sonuc = await baglanti.PastaneUrunSilAsync(silinecek.PId);
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
}