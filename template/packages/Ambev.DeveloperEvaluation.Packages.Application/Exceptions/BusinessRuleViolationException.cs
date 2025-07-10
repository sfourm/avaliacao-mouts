using Ambev.DeveloperEvaluation.Packages.Application.Utils;

namespace Ambev.DeveloperEvaluation.Packages.Application.Exceptions;

public class BusinessRuleViolationException : Exception
{
    private const string DefaultMessage = "A business rule violation has occurred.";

    public BusinessRuleViolationException() : base(DefaultMessage)
    {
    }

    public BusinessRuleViolationException(string message) : base(message)
    {
    }

    public BusinessRuleViolationException(string message, Exception inner) : base(message, inner)
    {
    }

    public static void When(bool condition, string message = "")
    {
        if (condition) throw new BusinessRuleViolationException(ExceptionHelper.StringMessage(message, DefaultMessage));
    }
}