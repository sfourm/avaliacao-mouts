using Ambev.DeveloperEvaluation.Packages.WebApi.Filters.Interfaces;
using Ambev.DeveloperEvaluation.Packages.WebApi.Filters.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.Packages.WebApi.Filters.Handlers;

public class UnauthorizedAccessExceptionHandler : IExceptionHandler, IStaticExceptionHandler
{
    public Type HandleType => typeof(UnauthorizedAccessException);

    IActionResult IExceptionHandler.Handle(ExceptionContextWrapper context)
    {
        return Handle(context);
    }

    public static IActionResult Handle(ExceptionContextWrapper context)
    {
        var details = GetUnauthorizedProblemDetails(context.Exception.Message);

        return new UnauthorizedObjectResult(details);
    }

    public static ProblemDetails GetUnauthorizedProblemDetails(string detail)
    {
        return new ProblemDetails
        {
            Type = "https://datatracker.ietf.org/doc/html/rfc7235#section-3.1",
            Title = "Unauthorized.",
            Detail = detail,
            Status = StatusCodes.Status401Unauthorized
        };
    }
}