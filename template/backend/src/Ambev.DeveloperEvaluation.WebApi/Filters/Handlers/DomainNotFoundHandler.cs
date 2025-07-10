using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.Packages.WebApi.Filters.Interfaces;
using Ambev.DeveloperEvaluation.Packages.WebApi.Filters.Models;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Filters.Handlers;

public class DomainNotFoundHandler : IExceptionHandler
{
    public Type HandleType => typeof(NotFoundDomainException);

    public IActionResult Handle(ExceptionContextWrapper context)
    {
        ProblemDetails details = new()
        {
            Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.4",
            Title = "The specified resource was not found.",
            Status = StatusCodes.Status404NotFound,
            Detail = context.Exception.Message
        };

        return new NotFoundObjectResult(details);
    }
}