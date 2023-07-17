using Profile;
using Tool.Notifications;
using Tool.PushNotifications.Settings;

namespace Tool.PushNotifications
{
    class NotificationController : BaseController
    {
        private PlayerProfile _profilePlayer;
        private NotificationSettings _settings;
        private NotificationManager _notificationManager;

        public NotificationController(PlayerProfile profilePlayer, NotificationSettings settings)
        {
            _profilePlayer = profilePlayer;
            _settings = settings;

            _notificationManager = new NotificationManager();
            _notificationManager.CreateNotification(_settings);
        }
    }
}
