namespace Ambev.DeveloperEvaluation.Domain.Exceptions;

public class RuleViolationDomainException(string message)
    : DomainException(message.IsEmpty() ? DefaultMessage : message)
{
    private const string DefaultMessage = "A domain rule was violated.";
}