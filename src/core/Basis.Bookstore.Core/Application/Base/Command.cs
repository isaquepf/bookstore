using Flunt.Notifications;
using MediatR;

namespace Basis.Bookstore.Core.Application.Base
{
    public abstract class Command<T> : Notifiable, IRequest<Result>
     where T : Command<T>
         , new()
    {
        private ErrorCode ErrorCode { get; set; } = ErrorCode.Business;
        public virtual void DefaultErrorValidationResponse(ErrorCode errorCode) => ErrorCode = errorCode;
        public virtual ErrorCode GetErrorValidationResponse() => ErrorCode;
    }
}
