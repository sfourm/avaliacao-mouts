using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.Packages.WebApi.Filters.Interfaces;
using Ambev.DeveloperEvaluation.Packages.WebApi.Filters.Models;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Filters.Handlers;

public class DomainRuleViolationHandler : IExceptionHandler
{
    public Type HandleType => typeof(RuleViolationDomainException);

    public IActionResult Handle(ExceptionContextWrapper context)
    {
        ProblemDetails details = new()
        {
            Status = StatusCodes.Status422UnprocessableEntity,
            Title = "An internal rule was violated.",
            Type = "https://datatracker.ietf.org/doc/html/rfc4918#section-11.2",
            Detail = context.Exception.Message
        };

        return new UnprocessableEntityObjectResult(details);
    }
}