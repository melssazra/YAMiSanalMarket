namespace YAMiMAUI
{
    public partial class App : Application
    {
        public static string GirisYapanPersonelAdSoyad { get; set; }
        public App()
        {
            InitializeComponent();

            try
            {
                // MauiApp.Current.Services, MauiProgram'da oluşturulan IServiceProvider'dır.
                // MainPage'i bağımlılık enjeksiyonu ile çözümleyerek alın.
                MainPage = new NavigationPage(MauiProgram.CreateMauiApp().Services.GetService<MainPage>());
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Hata: {ex.Message}\n{ex.StackTrace}");
                MainPage = new ContentPage
                {
                    Content = new Label
                    {
                        Text = $"Başlangıç hatası:\n{ex.Message}",
                        TextColor = Colors.Red,
                        VerticalOptions = LayoutOptions.Center,
                        HorizontalOptions = LayoutOptions.Center
                    }
                };
            }
        }
    }
}