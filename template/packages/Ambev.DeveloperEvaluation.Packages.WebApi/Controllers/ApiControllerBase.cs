using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace Ambev.DeveloperEvaluation.Packages.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
[Produces("application/json")]
public abstract class ApiControllerBase : ControllerBase
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("/error")]
    public IActionResult HandleError([FromServices] IHostEnvironment hostEnvironment)
    {
        if (!hostEnvironment.IsDevelopment()) return Problem();

        var exceptionHandlerFeature = HttpContext.Features.Get<IExceptionHandlerFeature>()!;

        return Problem(exceptionHandlerFeature.Error.StackTrace, title: exceptionHandlerFeature.Error.Message);
    }
}