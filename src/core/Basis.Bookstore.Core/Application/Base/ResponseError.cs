using Basis.Bookstore.Core.Application.Base;

namespace VenturesLab.BacklogTasks.Core.Application.Base
{
    public class ResponseError<TNotifications>
    {
        public ErrorCode? Code { get; private set; }

        public Guid Id { get; private set; }

        public string Message { get; private set; }

        public IEnumerable<TNotifications> Errors { get; private set; }

        public ResponseError()
        {
        }

        public ResponseError(Guid id, ErrorCode? code, IEnumerable<TNotifications> errors, string message)
        {
            Id = id;
            Code = code;
            Errors = errors;
            Message = message;
        }

        public ResponseError<TNotifications> FromResult<T>(ResponseData<T, TNotifications> responseData)
        {
            Id = responseData.Id;
            Code = responseData.Code;
            Errors = responseData.Errors;
            Message = responseData.Message;
            return this;
        }

    }
}