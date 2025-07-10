using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Ambev.DeveloperEvaluation.Packages.WebApi.Filters.Models;

public class ExceptionContextWrapper
{
    private readonly ExceptionContext _exceptionContext;

    public ExceptionContextWrapper(ExceptionContext exceptionContext)
    {
        _exceptionContext = exceptionContext;
    }

    public Exception Exception => _exceptionContext.Exception;

    public ModelStateDictionary ModelState => _exceptionContext.ModelState;

    public void SetResult(IActionResult actionResult)
    {
        _exceptionContext.Result = actionResult;
        _exceptionContext.ExceptionHandled = true;
    }

    public TException As<TException>() where TException : Exception
    {
        return (TException)_exceptionContext.Exception;
    }

    public static implicit operator ExceptionContextWrapper(ExceptionContext exceptionContext)
    {
        return new ExceptionContextWrapper(exceptionContext);
    }
}