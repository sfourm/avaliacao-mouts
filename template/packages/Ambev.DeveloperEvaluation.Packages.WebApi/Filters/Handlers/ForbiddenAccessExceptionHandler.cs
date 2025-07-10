using Ambev.DeveloperEvaluation.Packages.Application.Exceptions;
using Ambev.DeveloperEvaluation.Packages.WebApi.Filters.Interfaces;
using Ambev.DeveloperEvaluation.Packages.WebApi.Filters.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.Packages.WebApi.Filters.Handlers;

public class ForbiddenAccessExceptionHandler : IExceptionHandler, IStaticExceptionHandler
{
    public Type HandleType => typeof(ForbiddenAccessException);

    IActionResult IExceptionHandler.Handle(ExceptionContextWrapper context)
    {
        return Handle(context);
    }

    public static IActionResult Handle(ExceptionContextWrapper context)
    {
        var details = GetDetails(context.Exception.Message);

        return new ObjectResult(details) { StatusCode = StatusCodes.Status403Forbidden };
    }

    public static ProblemDetails GetDetails(string detail)
    {
        return new ProblemDetails
        {
            Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.3",
            Title = "Forbidden.",
            Detail = detail,
            Status = StatusCodes.Status403Forbidden
        };
    }
}