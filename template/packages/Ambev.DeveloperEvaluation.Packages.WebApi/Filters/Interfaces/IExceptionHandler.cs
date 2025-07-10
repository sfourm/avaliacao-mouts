using Ambev.DeveloperEvaluation.Packages.WebApi.Filters.Models;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.Packages.WebApi.Filters.Interfaces;

public interface IExceptionHandlerType;

public interface IExceptionHandler : IExceptionHandlerType
{
    Type HandleType { get; }
    IActionResult Handle(ExceptionContextWrapper context);
}

public interface IStaticExceptionHandler : IExceptionHandlerType
{
    static abstract IActionResult Handle(ExceptionContextWrapper context);
}