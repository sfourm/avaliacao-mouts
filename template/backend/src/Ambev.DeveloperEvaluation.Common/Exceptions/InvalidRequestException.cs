
using Ambev.DeveloperEvaluation.Common.Utils;

namespace Ambev.DeveloperEvaluation.Common.Exceptions;

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
        if (condition)
        {
            throw new InvalidRequestException(ExceptionHelper.StringMessage(message, DefaultMessage));
        }
    }
}