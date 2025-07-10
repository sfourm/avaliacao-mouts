namespace Ambev.DeveloperEvaluation.Domain.Exceptions;

public class NotFoundDomainException(string message)
    : DomainException(message.IsEmpty() ? DefaultMessage : message)
{
    private const string DefaultMessage = "The specified resource was not found.";
}