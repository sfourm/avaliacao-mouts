using Ambev.DeveloperEvaluation.Packages.Application.Utils;

namespace Ambev.DeveloperEvaluation.Packages.Application.Exceptions;

public class SemanticValidationException : Exception
{
    private const string DefaultMessage = "A semantic validation error has occurred.";

    public SemanticValidationException()
    {
    }

    public SemanticValidationException(string message) : base(message)
    {
    }

    public SemanticValidationException(string message, Exception inner) : base(message, inner)
    {
    }

    public static void When(bool condition, string message = "")
    {
        if (condition) throw new SemanticValidationException(ExceptionHelper.StringMessage(message, DefaultMessage));
    }
}