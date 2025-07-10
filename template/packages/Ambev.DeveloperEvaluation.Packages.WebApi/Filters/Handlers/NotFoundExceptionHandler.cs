using Ambev.DeveloperEvaluation.Packages.Application.Exceptions;
using Ambev.DeveloperEvaluation.Packages.WebApi.Filters.Interfaces;
using Ambev.DeveloperEvaluation.Packages.WebApi.Filters.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.Packages.WebApi.Filters.Handlers;

public class NotFoundExceptionHandler : IExceptionHandler, IStaticExceptionHandler
{
    public Type HandleType => typeof(NotFoundException);

    IActionResult IExceptionHandler.Handle(ExceptionContextWrapper context)
    {
        return Handle(context);
    }

    public static IActionResult Handle(ExceptionContextWrapper context)
    {
        var details = GetDetails(context.Exception.Message);

        return new NotFoundObjectResult(details);
    }

    public static ProblemDetails GetDetails(string detail)
    {
        return new ProblemDetails
        {
            Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.4",
            Title = "The specified resource was not found.",
            Detail = detail,
            Status = StatusCodes.Status404NotFound
        };
    }
}