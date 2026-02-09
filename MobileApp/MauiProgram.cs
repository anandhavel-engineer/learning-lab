using Microsoft.Extensions.Logging;
using Microsoft.Maui.LifecycleEvents;

using Plugin.Firebase.Bundled.Shared;
using Plugin.Firebase.Bundled.Platforms.Android;

namespace MobileApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .RegisterFirebaseServices()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }

        private static MauiAppBuilder RegisterFirebaseServices(this MauiAppBuilder builder)
        {
            builder.ConfigureLifecycleEvents(events =>
            {
#if ANDROID
                events.AddAndroid(android => android.OnCreate((activity, _) =>
                    CrossFirebase.Initialize(
                        activity,
                        () => activity,
                        CreateCrossFirebaseSettings()
                    )));
#elif IOS
                events.AddiOS(iOS => iOS.FinishedLaunching((_, _) =>
                {
                    CrossFirebase.Initialize(CreateCrossFirebaseSettings());
                    return false;
                }));
#endif
            });

            return builder;
        }

        private static CrossFirebaseSettings CreateCrossFirebaseSettings()
        {
            return new CrossFirebaseSettings(
                isAuthEnabled: false,
                isCloudMessagingEnabled: true,
                isCrashlyticsEnabled: false
            );
        }
    }
}
