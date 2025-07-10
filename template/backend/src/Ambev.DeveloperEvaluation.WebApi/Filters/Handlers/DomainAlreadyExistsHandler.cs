using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.Packages.WebApi.Filters.Interfaces;
using Ambev.DeveloperEvaluation.Packages.WebApi.Filters.Models;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Filters.Handlers;

public class DomainAlreadyExistsHandler : IExceptionHandler
{
    public Type HandleType => typeof(AlreadyExistsDomainException);

    public IActionResult Handle(ExceptionContextWrapper context)
    {
        ProblemDetails details = new()
        {
            Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.8",
            Title = "The specified resource already exists.",
            Status = StatusCodes.Status409Conflict,
            Detail = context.Exception.Message
        };

        return new ConflictObjectResult(details);
    }
}