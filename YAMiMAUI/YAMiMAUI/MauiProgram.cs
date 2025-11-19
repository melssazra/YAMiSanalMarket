using Microsoft.Extensions.Logging;
using YAMiMAUI.Baglanti;
using YAMiMAUI.GirisEkrani;
using YAMiMAUI.Kategoriler;
using YAMiMAUI.KategoriSecme;
using YAMiMAUI.Services;
using YAMiMAUI.UyelikSayfasi;
using YAMiMAUI.Validation;
using YAMiMAUI.ViewModels;

namespace YAMiMAUI
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            // Servisleri kaydedin (Singleton olarak, uygulama ömrü boyunca tek örnek)
            builder.Services.AddSingleton<HttpClient>(); // HttpClient eklendi
            builder.Services.AddSingleton<APIBaglanti>();
            builder.Services.AddSingleton<MusteriBaglanti>();
            builder.Services.AddSingleton<SepetBaglanti>();
            builder.Services.AddSingleton<SepetServis>();
            
            builder.Services.AddSingleton<Kategoriler.Sepetim>();
            builder.Services.AddSingleton<Kategoriler.PersonelYiyecekVeIcecek>();
            builder.Services.AddSingleton<Kategoriler.PersonelPastane>();
            builder.Services.AddSingleton<Kategoriler.PersonelMeyveVeSebze>();
            builder.Services.AddSingleton<Kategoriler.PersonelKozmetik>();
            builder.Services.AddSingleton<Kategoriler.MusteriYiyecekVeIcecek>();
            builder.Services.AddSingleton<Kategoriler.MusteriPastane>();
            builder.Services.AddSingleton<Kategoriler.MusteriMeyveVeSebze>();
            builder.Services.AddSingleton<Kategoriler.MusteriKozmetik>();
            builder.Services.AddSingleton<Kategoriler.MusteriBilgileri>();
            builder.Services.AddSingleton<Kategoriler.Bilgilerim>();

            builder.Services.AddSingleton<Baglanti.YiyecekVeIcecekBaglanti>();
            builder.Services.AddSingleton<Baglanti.KozmetikBaglanti>();
            builder.Services.AddSingleton<Baglanti.MeyveVeSebzeBaglanti>();
            builder.Services.AddSingleton<Baglanti.PastaneBaglanti>();
            builder.Services.AddSingleton<Baglanti.PersonelBaglanti>();


            builder.Services.AddSingleton<SepetimViewModel>(); // ViewModel
            builder.Services.AddSingleton<SepetimValidator>(); // Yeni
            builder.Services.AddSingleton<SiparisHandler>(); // Yeni


            // Sayfaları kaydedin (Transient olarak, her istendiğinde yeni örnek)
            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<MusteriGiris>();
            builder.Services.AddTransient<UyelikSayfasi.UyelikSayfasi>();
            builder.Services.AddTransient<MusteriKategori>();
            builder.Services.AddTransient<Kategoriler.Bilgilerim>();
            builder.Services.AddTransient<Kategoriler.MusteriPastane>();
            builder.Services.AddTransient<Kategoriler.Sepetim>();
            // Diğer kategori sayfalarını da ekleyin (MusteriYiyecekVeIcecek, MusteriMeyveVeSebze, MusteriKozmetik vb.)
            builder.Services.AddTransient<Kategoriler.MusteriYiyecekVeIcecek>(); // Eğer bu sayfalar varsa ve DI kullanıyorlarsa
            builder.Services.AddTransient<Kategoriler.MusteriMeyveVeSebze>();
            builder.Services.AddTransient<Kategoriler.MusteriKozmetik>();
            builder.Services.AddTransient<Kategoriler.MusteriBilgileri>();
            builder.Services.AddTransient<Kategoriler.PersonelKozmetik>();
            builder.Services.AddTransient<Kategoriler.PersonelMeyveVeSebze>();
            builder.Services.AddTransient<Kategoriler.PersonelPastane>();
            builder.Services.AddTransient<Kategoriler.PersonelYiyecekVeIcecek>();
            builder.Services.AddTransient<KozmetikBaglanti>();


#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}