using Ambev.DeveloperEvaluation.Packages.Application.Exceptions;
using Ambev.DeveloperEvaluation.Packages.WebApi.Filters.Interfaces;
using Ambev.DeveloperEvaluation.Packages.WebApi.Filters.Models;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.Packages.WebApi.Filters.Handlers;

public class ValidationExceptionHandler : IExceptionHandler
{
    public Type HandleType => typeof(ValidationException);

    public IActionResult Handle(ExceptionContextWrapper context)
    {
        var exception = context.As<ValidationException>();

        ValidationProblemDetails details = new(exception.Errors)
        {
            Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1"
        };

        return new BadRequestObjectResult(details);
    }
}