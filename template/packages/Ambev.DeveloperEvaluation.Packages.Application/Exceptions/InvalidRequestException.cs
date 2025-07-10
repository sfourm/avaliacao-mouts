using Ambev.DeveloperEvaluation.Packages.Application.Utils;

namespace Ambev.DeveloperEvaluation.Packages.Application.Exceptions;

public class InvalidRequestException : Exception
{
    private const string DefaultMessage = "Invalid request.";

    public InvalidRequestException()
    {
    }

    public InvalidRequestException(string message) : base(message)
    {
    }

    public InvalidRequestException(string message, Exception innerException) : base(message, innerException)
    {
    }

    public static void When(bool condition, string message = "")
    {
        if (condition) throw new InvalidRequestException(ExceptionHelper.StringMessage(message, DefaultMessage));
    }
}