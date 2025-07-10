using Ambev.DeveloperEvaluation.Packages.Application.Exceptions;
using Ambev.DeveloperEvaluation.Packages.WebApi.Filters.Interfaces;
using Ambev.DeveloperEvaluation.Packages.WebApi.Filters.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.Packages.WebApi.Filters.Handlers;

public class AlreadyExistsExceptionHandler : IExceptionHandler, IStaticExceptionHandler
{
    public Type HandleType => typeof(AlreadyExistsException);

    IActionResult IExceptionHandler.Handle(ExceptionContextWrapper context)
    {
        return Handle(context);
    }

    public static IActionResult Handle(ExceptionContextWrapper context)
    {
        var details = GetDetails(context.Exception.Message);

        return new ConflictObjectResult(details);
    }

    public static ProblemDetails GetDetails(string detail)
    {
        return new ProblemDetails
        {
            Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.8",
            Title = "The specified resource already exists.",
            Detail = detail,
            Status = StatusCodes.Status409Conflict
        };
    }
}