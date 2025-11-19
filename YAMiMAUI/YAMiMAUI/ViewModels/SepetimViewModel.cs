using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using YAMiMAUI.DTO;
using YAMiMAUI.Services;
using YAMiMAUI.Siniflar;
using YAMiMAUI.Baglanti;
using YAMiMAUI.Validation; // Yeni Validator namespace'i
using Microsoft.Maui.Controls;

namespace YAMiMAUI.ViewModels
{
    public class SepetimViewModel : INotifyPropertyChanged
    {
        // Bağımlılıklar (Services ve Yeni Ayırıcılar)
        private readonly SepetServis _sepetServis;
        private readonly SepetimValidator _validator;   // Doğrulama Sorumluluğu
        private readonly SiparisHandler _siparisHandler; // Sipariş İş Akışı Sorumluluğu

        // Private Alanlar (Fields)
        private decimal _toplamTutar;
        private bool _isKrediKartiSelected;
        private bool _isNakitSelected;
        private DateTime _selectedDate;
        private TimeSpan _selectedTime;
        private bool _isTermsAccepted;

        // UI'ya bağlı Veri Koleksiyonu
        public ObservableCollection<SepetListeDTO> SepetItems => _sepetServis.SepetItems;

        // Komutlar
        public ICommand KaldirCommand { get; }
        public ICommand OdemeyiTamamlaCommand { get; }
        public ICommand SepetiTemizleCommand { get; }
        public ICommand ShowTermsCommand { get; }

        // Constructor - Yeni bağımlılıklar eklendi
        public SepetimViewModel(
            SepetServis sepetServis,
            SepetBaglanti sepetBaglanti, // SepetiTemizle'de kullanılabilir veya Handler'a dahil edilebilir
            SiparisHandler siparisHandler,
            SepetimValidator validator)
        {
            _sepetServis = sepetServis;
            _siparisHandler = siparisHandler;
            _validator = validator;

            // Varsayılan değerler
            SelectedDate = DateTime.Today;
            SelectedTime = DateTime.Now.TimeOfDay;

            // Komut atamaları
            KaldirCommand = new Command<Guid>(async (sepetId) => await OnUrunKaldirAsync(sepetId));
            OdemeyiTamamlaCommand = new Command(async () => await OdemeyiTamamlaAsync(), CanOdemeyiTamamla);
            SepetiTemizleCommand = new Command(async () => await OnSepetiTemizleAsync());
            ShowTermsCommand = new Command(async () => await Application.Current.MainPage.DisplayAlert("Kullanım ve İade Koşulları", "Burada kullanım ve iade koşulları yer almaktadır.", "Tamam"));

            // Sepet listesi değiştiğinde toplam tutarı otomatik güncelle
            _sepetServis.SepetItems.CollectionChanged += (s, e) => CalculateToplamTutar();
            CalculateToplamTutar();
        }

        // --- Özellikler (Properties) ---

        public decimal ToplamTutar
        {
            get => _toplamTutar;
            set
            {
                if (_toplamTutar != value)
                {
                    _toplamTutar = value;
                    OnPropertyChanged();
                    ((Command)OdemeyiTamamlaCommand).ChangeCanExecute();
                }
            }
        }

        public bool IsKrediKartiSelected
        {
            get => _isKrediKartiSelected;
            set
            {
                if (_isKrediKartiSelected != value)
                {
                    _isKrediKartiSelected = value;
                    if (value) IsNakitSelected = false;
                    OnPropertyChanged();
                    ((Command)OdemeyiTamamlaCommand).ChangeCanExecute();
                }
            }
        }

        public bool IsNakitSelected
        {
            get => _isNakitSelected;
            set
            {
                if (_isNakitSelected != value)
                {
                    _isNakitSelected = value;
                    if (value) IsKrediKartiSelected = false;
                    OnPropertyChanged();
                    ((Command)OdemeyiTamamlaCommand).ChangeCanExecute();
                }
            }
        }

        public bool IsTermsAccepted
        {
            get => _isTermsAccepted;
            set
            {
                if (_isTermsAccepted != value)
                {
                    _isTermsAccepted = value;
                    OnPropertyChanged();
                    ((Command)OdemeyiTamamlaCommand).ChangeCanExecute();
                }
            }
        }

        public DateTime SelectedDate
        {
            get => _selectedDate;
            set
            {
                if (_selectedDate != value)
                {
                    _selectedDate = value;
                    OnPropertyChanged();
                }
            }
        }

        public TimeSpan SelectedTime
        {
            get => _selectedTime;
            set
            {
                if (_selectedTime != value)
                {
                    _selectedTime = value;
                    OnPropertyChanged();
                }
            }
        }

        // --- Yeni EKLENEN Metot ---
        public async Task YukleAsync()
        {
            if (Oturum.MusteriId != Guid.Empty)
            {
                await _sepetServis.SepetiYukleAsync(Oturum.MusteriId);
            }
        }
        // -------------------------

        // --- İş Mantığı Metotları (Daha Önceki Gibi) ---

        private void CalculateToplamTutar()
        {
            ToplamTutar = _sepetServis.SepetItems?.Sum(item => item.Tutar) ?? 0;
        }

        private async Task OnUrunKaldirAsync(Guid sepetId)
        {
            // Basit sepet/ürün yönetimi, servis içinde kalabilir.
            Guid musteriId = Oturum.MusteriId;
            if (musteriId == Guid.Empty)
            {
                await Application.Current.MainPage.DisplayAlert("Hata", "Müşteri bilgisi alınamadı. Lütfen tekrar giriş yapın.", "Tamam");
                return;
            }
            await _sepetServis.SepettenUrunKaldirAsync(sepetId, musteriId);
        }

        private async Task OnSepetiTemizleAsync()
        {
            var result = await Application.Current.MainPage.DisplayAlert("Sepeti Temizle", "Sepeti tamamen temizlemek istediğinize emin misiniz?", "Evet", "Hayır");
            if (result)
            {
                Guid musteriId = Oturum.MusteriId;
                if (musteriId == Guid.Empty)
                {
                    await Application.Current.MainPage.DisplayAlert("Hata", "Müşteri bilgisi alınamadı. Lütfen tekrar giriş yapın.", "Tamam");
                    return;
                }
                await _sepetServis.SepetiTemizleAsync(musteriId);
            }
        }

        private bool CanOdemeyiTamamla()
        {
            return IsTermsAccepted && ToplamTutar > 0 && (IsKrediKartiSelected || IsNakitSelected);
        }

        // --- İş Akışı Parçalandı ---

        private async Task OdemeyiTamamlaAsync()
        {
            // 1. DOĞRULAMA (Validator'a delege edildi)
            string validationMessage = _validator.Validate(this);
            if (validationMessage != null)
            {
                await Application.Current.MainPage.DisplayAlert("Uyarı", validationMessage, "Tamam");
                return;
            }

            Guid musteriId = Oturum.MusteriId;
            string paymentMethod = IsKrediKartiSelected ? "Kredi Kartı" : "Nakit Ödeme";

            // 2. İŞLEM (Handler'a delege edildi)
            bool siparisBasarili = await _siparisHandler.TamamlaAsync(musteriId, paymentMethod, SelectedDate, SelectedTime);

            // 3. SONUÇ BİLDİRİMİ
            if (siparisBasarili)
            {
                await Application.Current.MainPage.DisplayAlert("Sipariş Onayı", $"Siparişiniz başarıyla alındı ve {paymentMethod} ile onaylandı.", "Tamam");
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Hata", "Siparişinizi tamamlarken bir sorun oluştu (API veya stok hatası). Lütfen tekrar deneyiniz.", "Tamam");
            }
        }

        // --- INotifyPropertyChanged Implementasyonu ---
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Bellek sızıntılarını önlemek için temizleme metodu
        public void Dispose()
        {
            if (_sepetServis.SepetItems != null)
            {
                _sepetServis.SepetItems.CollectionChanged -= (s, e) => CalculateToplamTutar();
            }
        }
    }
}