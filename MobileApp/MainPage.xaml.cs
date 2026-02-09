using MobileApp.Pages;

using Plugin.Firebase.CloudMessaging;

namespace MobileApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            FCMTokenSection.IsVisible = false;
        }

        private async void ColorMakerButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ColorMaker());
        }

        private void PerfectShareButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PerfectShare());
        }

        private void CodeQuotesButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CodeQuotes());
        }

        private async void GenerateFCMToken_Clicked(object sender, EventArgs e)
        {
            FCMTokenSection.IsVisible = true;

            await CrossFirebaseCloudMessaging.Current.CheckIfValidAsync();
            var token = await CrossFirebaseCloudMessaging.Current.GetTokenAsync();

            Console.WriteLine($"FCM token: {token}");
            FCMTokenEditor.Text = token;
        }

    }
}
