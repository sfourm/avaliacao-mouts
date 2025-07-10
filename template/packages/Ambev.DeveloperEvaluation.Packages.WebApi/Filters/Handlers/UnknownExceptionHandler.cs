using Ambev.DeveloperEvaluation.Packages.WebApi.Filters.Interfaces;
using Ambev.DeveloperEvaluation.Packages.WebApi.Filters.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.Packages.WebApi.Filters.Handlers;

public class UnknownExceptionHandler : IStaticExceptionHandler
{
    public static IActionResult Handle(ExceptionContextWrapper context)
    {
        var details =
            GetUnknownProblemDetails(context.Exception.InnerException?.Message ?? context.Exception.Message);

        return new ObjectResult(details) { StatusCode = StatusCodes.Status500InternalServerError };
    }

    public static ProblemDetails GetUnknownProblemDetails(string detail)
    {
        return new ProblemDetails
        {
            Status = StatusCodes.Status500InternalServerError,
            Title = "An error occurred while processing your request.",
            Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.1",
            Detail = detail
        };
    }
}