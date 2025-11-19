namespace YAMiMAUI.KategoriSecme;

public partial class PersonelKategori : ContentPage
{
	public PersonelKategori()
	{
		InitializeComponent();
	}
    private async void YiyecekVeIcecekClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Kategoriler.PersonelYiyecekVeIcecek());
    }
    private async void MeyveVeSebzeClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Kategoriler.PersonelMeyveVeSebze());
    }
    private async void KozmetikClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Kategoriler.PersonelKozmetik());
    }
    private async void PastaneClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Kategoriler.PersonelPastane());
    }
    private async void MusteriBilgileriClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Kategoriler.MusteriBilgileri());
    }

}