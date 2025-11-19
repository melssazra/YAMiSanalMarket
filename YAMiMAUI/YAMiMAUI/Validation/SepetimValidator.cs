using YAMiMAUI.Siniflar; // Oturum sınıfı için gerekli
using YAMiMAUI.ViewModels; // ViewModel'in kendisine erişmek için gerekli

namespace YAMiMAUI.Validation
{
    // Bu sınıfı projenizde yeni bir 'Validation' klasöründe veya 'Siniflar' içinde oluşturabilirsiniz.
    public class SepetimValidator
    {
        public string Validate(SepetimViewModel viewModel)
        {
            if (Oturum.MusteriId == Guid.Empty)
            {
                return "Müşteri bilgisi alınamadı. Lütfen tekrar giriş yapın.";
            }
            if (!viewModel.IsTermsAccepted)
            {
                return "İşlemi tamamlamak için kullanım ve iade koşullarını kabul etmelisiniz.";
            }
            if (viewModel.ToplamTutar <= 0)
            {
                return "Sepetinizde ürün bulunmamaktadır.";
            }
            if (!viewModel.IsKrediKartiSelected && !viewModel.IsNakitSelected)
            {
                return "Lütfen bir ödeme yöntemi seçiniz.";
            }
            if (viewModel.SelectedDate.Date < DateTime.Today.Date)
            {
                return "Geçmiş bir teslimat tarihi seçilemez.";
            }

            return null; // Doğrulama başarılı
        }
    }
}