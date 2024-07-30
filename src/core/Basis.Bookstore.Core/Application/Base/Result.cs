using Flunt.Notifications;

namespace Basis.Bookstore.Core.Application.Base
{
    public class Result : Notifiable
    {
        public Result()
        {
        }

        public Result(string error) => AddNotification(null, error);

        public Result(IReadOnlyCollection<Notification> notifications) => AddNotifications(notifications);

        public object Data { get; set; }

        public void AddNotifications(IReadOnlyCollection<Notification> notifications, ErrorCode errorCode)
        {
            AddNotifications(notifications);
            Error = errorCode;
        }

        public void AddNotification(string error, ErrorCode errorCode)
        {
            AddNotification(null, error);
            Error = errorCode;
        }

        public void AddNotification(string property, string message, ErrorCode errorCode)
        {
            AddNotification(property, message);
            Error = errorCode;
        }

        public void AddNotification(string error) => AddNotification(null, error);

        public void AddNotification(Notification notification, ErrorCode errorCode)
        {
            AddNotification(notification);
            Error = errorCode;
        }

        public ErrorCode? Error { get; set; }
    }

}
