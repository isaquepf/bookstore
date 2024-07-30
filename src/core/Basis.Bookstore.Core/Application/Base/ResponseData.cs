using System.Diagnostics;
using System.Text.Json.Serialization;

namespace Basis.Bookstore.Core.Application.Base
{
    public class ResponseData<T, TNotifications>
    {
        public ErrorCode? Code { get; set; }

        public Guid Id { get; } = GetId();


        public string Message => GetMessage();

        [JsonIgnore]
        public bool Invalid
        {
            get
            {
                if (Errors != null)
                {
                    return Errors.Any();
                }

                return false;
            }
        }

        [JsonIgnore]
        public bool Valid
        {
            get
            {
                if (Errors != null)
                {
                    return !Errors.Any();
                }

                return true;
            }
        }

        public IEnumerable<TNotifications> Errors { get; set; }

        public T Data { get; set; }

        private ResponseData()
        {
        }

        public ResponseData(T data)
        {
            Data = data;
        }

        public ResponseData(TNotifications notification)
        {
            Errors = new List<TNotifications> { notification };
        }

        public ResponseData(IEnumerable<TNotifications> notifications)
        {
            Errors = notifications;
        }

        public ResponseData(IEnumerable<TNotifications> notifications, T data)
        {
            Data = data;
            Errors = notifications;
        }

        public ResponseData(IEnumerable<TNotifications> notifications, ErrorCode errorCode)
        {
            Errors = notifications;
            Code = errorCode;
        }

        private static Guid GetId()
        {
            if (!Guid.TryParse(Activity.Current?.RootId, out var result))
            {
                return Guid.NewGuid();
            }

            return result;
        }

        private string GetMessage()
        {
            return Code switch
            {
                ErrorCode.NotFound => "Resource not found",
                ErrorCode.BadRequest => "Invalid request parameters",
                ErrorCode.Business => "Business rules violated",
                ErrorCode.Unauthorized => "Unauthorized",
                ErrorCode.Conflict => "Resource already exists",
                ErrorCode.ServiceUnavailable => "Service is unavailable",
                _ => "An error has ocurred",
            };
        }
    }

}
