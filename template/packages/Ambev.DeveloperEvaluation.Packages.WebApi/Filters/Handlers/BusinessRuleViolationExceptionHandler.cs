using Ambev.DeveloperEvaluation.Packages.Application.Exceptions;
using Ambev.DeveloperEvaluation.Packages.WebApi.Filters.Interfaces;
using Ambev.DeveloperEvaluation.Packages.WebApi.Filters.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.Packages.WebApi.Filters.Handlers;

public class BusinessRuleViolationExceptionHandler : IExceptionHandler
{
    public Type HandleType => typeof(BusinessRuleViolationException);

    public IActionResult Handle(ExceptionContextWrapper context)
    {
        var details = GetDetails(context.Exception.Message);

        return new UnprocessableEntityObjectResult(details);
    }

    public static ProblemDetails GetDetails(string detail)
    {
        return new ProblemDetails
        {
            Status = StatusCodes.Status422UnprocessableEntity,
            Title = "Business Rule Violation.",
            Type = "https://datatracker.ietf.org/doc/html/rfc4918#section-11.2",
            Detail = detail
        };
    }
}