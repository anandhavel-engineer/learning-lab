using Foundation;

using UIKit;

using UserNotifications;

namespace MobileApp
{
    [Register("AppDelegate")]
    public class AppDelegate : MauiUIApplicationDelegate
    {
        protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();

        public override bool FinishedLaunching(UIApplication application, NSDictionary options)
        {
            // Required for foreground notifications
            UNUserNotificationCenter.Current.Delegate = new FirebaseNotificationDelegate();

            return base.FinishedLaunching(application, options);
        }
    }

    public class FirebaseNotificationDelegate : UNUserNotificationCenterDelegate
    {
        public override void WillPresentNotification(
            UNUserNotificationCenter center,
            UNNotification notification,
            Action<UNNotificationPresentationOptions> completionHandler)
        {
            completionHandler(
                UNNotificationPresentationOptions.Alert |
                UNNotificationPresentationOptions.Badge |
                UNNotificationPresentationOptions.Sound);
        }
    }
}
