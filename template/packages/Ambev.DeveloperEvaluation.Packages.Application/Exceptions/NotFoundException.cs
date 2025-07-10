using Ambev.DeveloperEvaluation.Packages.Application.Utils;

namespace Ambev.DeveloperEvaluation.Packages.Application.Exceptions;

public class NotFoundException : Exception
{
    private const string DefaultMessage = "Not found exception";

    public NotFoundException()
    {
    }

    public NotFoundException(string message) : base(message)
    {
    }

    public NotFoundException(string message, Exception innerException) : base(message, innerException)
    {
    }

    public NotFoundException(string name, object key) : base($"Entity \"{name}\" ({key}) was not found.")
    {
    }

    public static void When(bool condition, string message = "")
    {
        if (condition) throw new NotFoundException(ExceptionHelper.StringMessage(message, DefaultMessage));
    }
}