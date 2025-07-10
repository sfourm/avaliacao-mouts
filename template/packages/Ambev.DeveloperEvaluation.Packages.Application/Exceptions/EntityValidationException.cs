using Ambev.DeveloperEvaluation.Packages.Application.Utils;

namespace Ambev.DeveloperEvaluation.Packages.Application.Exceptions;

public class EntityValidationException : Exception
{
    private const string DefaultMessage = "A validation error has occurred in the entity.";

    public EntityValidationException()
    {
    }

    public EntityValidationException(string entityName, string error) : base(
        $"A validation error has occured in Entity \"{entityName}\": {error}")
    {
    }

    public EntityValidationException(string message) : base(message)
    {
    }

    public EntityValidationException(string message, Exception inner) : base(message, inner)
    {
    }

    public static void When(bool condition, string message = "")
    {
        if (condition) throw new EntityValidationException(ExceptionHelper.StringMessage(message, DefaultMessage));
    }
}