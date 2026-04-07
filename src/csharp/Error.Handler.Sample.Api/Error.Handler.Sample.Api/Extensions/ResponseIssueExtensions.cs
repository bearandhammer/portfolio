using Error.Handler.Sample.Api.Model.Validation;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Error.Handler.Sample.Api.Extensions
{
    public static class ResponseIssueExtensions
    {
        public static ProblemDetails ToProblemDetails(this ConflictingExerciseActivity conflictingExerciseActivity) =>  // TODO: Flesh out record...
            new()
            {
                Status = (int)HttpStatusCode.Conflict,
                Title = "Conflicting exercise activity detected",
                Type = "https://developer.mozilla.org/en-US/docs/Web/HTTP/Reference/Status/409",
                Detail = "An exercise activity is already logged for this date/time range. Please try again.",
                Extensions = { ["errorCode"] = "Resource.Conflicting" }
            };
    }
}
