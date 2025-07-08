
using Ambev.DeveloperEvaluation.Common.Utils;

namespace Ambev.DeveloperEvaluation.Common.Exceptions;

public class AlreadyExistsException : Exception
{
    private const string DefaultMessage = "Entity already exists.";

    public AlreadyExistsException()
    {
    }

    public AlreadyExistsException(string message) : base(message)
    {
    }

    public AlreadyExistsException(string message, Exception innerException) : base(message, innerException)
    {
    }

    public AlreadyExistsException(string name, object key) : base($"Entity \"{name}\" ({key}) already exists.")
    {
    }

    public static void When(bool condition, string message = "")
    {
        if (condition)
        {
            throw new AlreadyExistsException(ExceptionHelper.StringMessage(message, DefaultMessage));
        }
    }
}