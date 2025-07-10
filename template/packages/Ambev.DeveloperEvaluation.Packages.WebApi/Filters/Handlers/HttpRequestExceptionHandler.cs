using System.Net;
using Ambev.DeveloperEvaluation.Packages.WebApi.Filters.Interfaces;
using Ambev.DeveloperEvaluation.Packages.WebApi.Filters.Models;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.Packages.WebApi.Filters.Handlers;

public class HttpRequestExceptionHandler : IExceptionHandler
{
    public Type HandleType => typeof(HttpRequestException);

    public IActionResult Handle(ExceptionContextWrapper context)
    {
        return context.As<HttpRequestException>().StatusCode switch
        {
            HttpStatusCode.NotFound => NotFoundExceptionHandler.Handle(context),
            HttpStatusCode.Unauthorized => UnauthorizedAccessExceptionHandler.Handle(context),
            HttpStatusCode.Forbidden => ForbiddenAccessExceptionHandler.Handle(context),
            HttpStatusCode.Conflict => AlreadyExistsExceptionHandler.Handle(context),
            HttpStatusCode.PaymentRequired => QuotaExceededExceptionHandler.Handle(context),
            HttpStatusCode.BadRequest => BadRequestExceptionHandler.Handle(context),
            HttpStatusCode.UnprocessableEntity => UnprocessableEntityExceptionHandler.Handle(context),
            _ => UnknownExceptionHandler.Handle(context)
        };
    }
}