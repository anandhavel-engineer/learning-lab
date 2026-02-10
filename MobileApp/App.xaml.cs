using Plugin.Firebase.Crashlytics;

namespace MobileApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // Initialize Firebase Cloud Messaging
            Task.Run(async () =>
            {
                try
                {
                    CrossFirebaseCrashlytics.Current.SetCrashlyticsCollectionEnabled(true);
                    await NotificationBootstrapService.InitializeAsync();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Firebase initialization error: {ex.Message}");
                }
            });
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            //return new Window(new AppShell());
            return new Window(new NavigationPage(new MainPage()));
        }
    }
}