using MobileApp.Pages;

namespace MobileApp
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private async void ColorMakerButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ColorMaker());
        }

        private void PerfectShareButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PerfectShare());
        }
    }
}
