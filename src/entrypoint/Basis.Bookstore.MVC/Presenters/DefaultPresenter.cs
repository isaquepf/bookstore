using Basis.Bookstore.Core.Application.Base;
using Flunt.Notifications;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using VenturesLab.BacklogTasks.Core.Application.Base;

namespace Basis.Bookstore.Api.Presenters
{
    public static class DefaultPresenter
    {
        public static IActionResult Cast(object result, HttpStatusCode statusCode)
            => Cast(new Result
            {
                Data = result
            }, statusCode);

        public static IActionResult Cast(Result result, HttpStatusCode statusCode)
        {
            if (result.Error.HasValue)
            {
                return result.Error.Value
                switch
                {
                    ErrorCode.NotFound => new NotFoundObjectResult(GetError(result)),
                    ErrorCode.Business => new UnprocessableEntityObjectResult(GetError(result)),
                    ErrorCode.Unauthorized => new UnauthorizedObjectResult(GetError(result)),
                    ErrorCode.InternalError => new ObjectResult(GetError(result))
                    {
                        StatusCode = (int)HttpStatusCode.InternalServerError
                    },
                    _ => new BadRequestObjectResult(GetError(result)),
                };
            }
            else
            {
                return statusCode
                switch
                {
                    HttpStatusCode.OK => new OkObjectResult(result.Data),
                    HttpStatusCode.Created => new CreatedResult(string.Empty, result.Data),
                    HttpStatusCode.NoContent => new NoContentResult(),
                    HttpStatusCode.Accepted => new AcceptedResult(string.Empty, result.Data),
                    _ => new OkResult()
                };
            }
        }

        public static ResponseError<Notification> GetError(Result resultado) =>
          new ResponseError<Notification>(Guid.NewGuid(), resultado.Error, resultado.Notifications, string.Empty);

    }

}
