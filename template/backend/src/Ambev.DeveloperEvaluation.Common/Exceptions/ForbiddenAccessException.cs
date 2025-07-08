
using Ambev.DeveloperEvaluation.Common.Utils;

namespace Ambev.DeveloperEvaluation.Common.Exceptions;

public class ForbiddenAccessException : Exception
{
    private const string DefaultMessage = "Forbidden Access.";

    public ForbiddenAccessException()
    {
    }

    public ForbiddenAccessException(string message) : base(message)
    {
    }

    public ForbiddenAccessException(string message, Exception innerException) : base(message, innerException)
    {
    }

    public static void When(bool condition, string message = "")
    {
        if (condition)
        {
            throw new ForbiddenAccessException(ExceptionHelper.StringMessage(message, DefaultMessage));
        }
    }
}