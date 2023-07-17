using Tool.PushNotifications;
using Tool.PushNotifications.Settings;

namespace Tool.Notifications
{
    internal class NotificationManager
    {
        private INotificationScheduler _scheduler;

        private void CreateNotifocationFactory(NotificationSettings settings)
        {
            var schedulerFactory = new NotificationSchedulerFactory(settings);
            _scheduler = schedulerFactory.Create();
        }

        public void CreateNotification(NotificationSettings settings)
        {
            CreateNotifocationFactory(settings);
            foreach (NotificationData notificationData in settings.Notifications)
                _scheduler.ScheduleNotification(notificationData);
        }
    }
}
