using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Error.Handler.Sample.Api.Handlers
{
    /// <summary>
    /// Global exception handler for the API.
    /// </summary>
    /// <param name="problemDetailsService">The <see cref="IProblemDetailsService"/> to handle the construction <see cref="ProblemDetails"/> types (as needed).</param>
    /// <param name="logger">The logger in scope for the operation.</param>
    /// <seealso cref="IExceptionHandler"/>
    public class GlobalExceptionHandler(
        IProblemDetailsService problemDetailsService,
        ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
    {
        /// <summary>
        /// Represents the logger in scope for the operation.
        /// </summary>
        private readonly ILogger<GlobalExceptionHandler> _logger = logger;

        /// <summary>
        /// Middleware method that attempts to handle any uncaught exception. This logs the exception and 
        /// returns a problem details object. This can be used to return a standardised error response to the client.
        /// </summary>
        /// <param name="httpContext">The <see cref="HttpContext"/> in scope for the operation.</param>
        /// <param name="exception">The <see cref="Exception"/> to handle (and push through logging and construct a <see cref="ProblemDetails"/> using).</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> in scope for the operation.</param>
        /// <returns>A <see cref="ValueTask{Boolean}"/> representing the asynchronous operation. True if the exception was handled, false otherwise.</returns>
        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            // TODO: Consider resource strings for general message logging, etc
            _logger.LogError(exception, "An unhandled exception occurred.");

            // Just illustrating that it is possible to pivot status code, if required, by Exception type
            httpContext.Response.StatusCode = exception switch
            {
                InvalidOperationException => StatusCodes.Status400BadRequest,
                _ => StatusCodes.Status500InternalServerError
            };

            return await problemDetailsService.TryWriteAsync(
                new ProblemDetailsContext
                {
                    HttpContext = httpContext,
                    Exception = exception,
                    ProblemDetails = new ProblemDetails
                    {
                        Type = exception.GetType().Name,    // Human readable docs...to be established (seen a mixture of text strings vs URIs)
                        Title = "An error occurred",
                        Detail = "An unhandled error has occurred, this has been logged."
                    }
                });
        }
    }
}
