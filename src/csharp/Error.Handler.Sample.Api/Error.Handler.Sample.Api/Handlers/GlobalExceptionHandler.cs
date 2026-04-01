using Microsoft.AspNetCore.Diagnostics;

namespace Error.Handler.Sample.Api.Handlers
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        public ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            // TODO: Implement and register
            throw new NotImplementedException();
        }
    }
}
