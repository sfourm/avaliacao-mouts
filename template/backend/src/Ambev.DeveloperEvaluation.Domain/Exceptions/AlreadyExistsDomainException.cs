namespace Ambev.DeveloperEvaluation.Domain.Exceptions;

public class AlreadyExistsDomainException(string message)
    : DomainException(message.IsEmpty() ? DefaultMessage : message)
{
    private const string DefaultMessage = "The specified resource already exists.";
}