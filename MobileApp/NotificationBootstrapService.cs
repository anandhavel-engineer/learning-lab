using Plugin.Firebase.CloudMessaging;

namespace MobileApp
{
    public static class NotificationBootstrapService
    {
        private const string TopicsSubscribedKey = "fcm_topics_subscribed";

        public static async Task InitializeAsync()
        {
            await EnsurePermissionAsync();
            await EnsureTopicsSubscribedAsync();
            RegisterForegroundHandlers();
        }

        // 1️⃣ PERMISSIONS
        private static async Task EnsurePermissionAsync()
        {
            var status = await Permissions.CheckStatusAsync<Permissions.PostNotifications>();

            if (status != PermissionStatus.Granted)
            {
                status = await Permissions.RequestAsync<Permissions.PostNotifications>();
            }

            // If still denied → respect user choice
        }

        // 2️⃣ TOPIC SUBSCRIPTION (ONCE)
        private static async Task EnsureTopicsSubscribedAsync()
        {
            if (Preferences.Get(TopicsSubscribedKey, false))
                return;

            var fcm = CrossFirebaseCloudMessaging.Current;

            await fcm.SubscribeToTopicAsync("All");

#if ANDROID
            await fcm.SubscribeToTopicAsync("Android");
#elif IOS
        await fcm.SubscribeToTopicAsync("ios");
#endif

            Preferences.Set(TopicsSubscribedKey, true);
        }

        // 3️⃣ FOREGROUND HANDLING
        private static void RegisterForegroundHandlers()
        {
            var fcm = CrossFirebaseCloudMessaging.Current;

            fcm.NotificationReceived += (_, e) =>
            {
                var title = e.Notification.Title;
                var body = e.Notification.Body;
            };

            fcm.TokenChanged += (_, token) =>
            {
                // Send token to backend if required
            };
        }
    }

}
