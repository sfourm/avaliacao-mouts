using Ambev.DeveloperEvaluation.Packages.WebApi.Filters.Handlers;
using Ambev.DeveloperEvaluation.Packages.WebApi.Filters.Interfaces;
using Ambev.DeveloperEvaluation.Packages.WebApi.Filters.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Ambev.DeveloperEvaluation.Packages.WebApi.Filters;

public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
{
    private readonly IEnumerable<IExceptionHandler> _handlers;

    public ApiExceptionFilterAttribute(IEnumerable<IExceptionHandler> handlers)
    {
        _handlers = handlers;
    }

    public override void OnException(ExceptionContext context)
    {
        HandleException(context);

        base.OnException(context);
    }

    private void HandleException(ExceptionContextWrapper context)
    {
        IActionResult result;
        var type = context.Exception.GetType();

        var handler = _handlers.FirstOrDefault(c => c.HandleType == type);

        if (handler is not null)
        {
            result = handler.Handle(context);
            context.SetResult(result);
            return;
        }

        result = UnknownExceptionHandler.Handle(context);
        context.SetResult(result);
    }
}