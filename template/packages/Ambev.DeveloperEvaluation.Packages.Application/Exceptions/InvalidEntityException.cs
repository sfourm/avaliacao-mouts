using Ambev.DeveloperEvaluation.Packages.Application.Utils;

namespace Ambev.DeveloperEvaluation.Packages.Application.Exceptions;

public class InvalidEntityException : Exception
{
    private const string DefaultMessage = "The entity is invalid.";

    public InvalidEntityException() : base(DefaultMessage)
    {
    }

    public InvalidEntityException(string message) : base(message)
    {
    }

    public InvalidEntityException(string message, Exception inner) : base(message, inner)
    {
    }

    public static void When(bool condition, string message = "")
    {
        if (condition) throw new InvalidEntityException(ExceptionHelper.StringMessage(message, DefaultMessage));
    }
}