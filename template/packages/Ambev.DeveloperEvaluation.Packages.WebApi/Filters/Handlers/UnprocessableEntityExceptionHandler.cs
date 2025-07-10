using Ambev.DeveloperEvaluation.Packages.WebApi.Filters.Interfaces;
using Ambev.DeveloperEvaluation.Packages.WebApi.Filters.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.Packages.WebApi.Filters.Handlers;

public abstract class UnprocessableEntityExceptionHandler : IStaticExceptionHandler
{
    public static IActionResult Handle(ExceptionContextWrapper context)
    {
        var details = GetUnprocessableEntityProblemDetails(context.Exception.Message);

        return new UnprocessableEntityObjectResult(details);
    }

    public static ProblemDetails GetUnprocessableEntityProblemDetails(string detail)
    {
        return new ProblemDetails
        {
            Status = StatusCodes.Status422UnprocessableEntity,
            Title = "Unprocessable Entity.",
            Type = "https://datatracker.ietf.org/doc/html/rfc4918#section-11.2",
            Detail = detail
        };
    }
}