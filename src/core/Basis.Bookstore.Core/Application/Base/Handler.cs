using MediatR;
using System.Text;

namespace Basis.Bookstore.Core.Application.Base
{
    public abstract class Handler<T> : IRequestHandler<T, Result>
       where T : Command<T>, new()
    {
        public Result Result { get; set; }

        public string HandlerName => typeof(T).Name;

        protected Handler()
        {
            Result = new Result();
        }

        public string ProcessName { get; set; } = nameof(T);

        public abstract Task<Result> Handle(T request, CancellationToken cancellationToken);

        protected string GetFormattedException(Exception error)
        {
            var errors = new StringBuilder();
            errors.AppendLine($"{HandlerName} handlerError Internal Server Error")
               .AppendLine($"msg: {error.Message}")
               .AppendLine($"st: {error.StackTrace}")
               .AppendLine($"in: {error?.InnerException?.Message}");
            return errors.ToString();
        }

    }
}
