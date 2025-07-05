using Ambev.DeveloperEvaluation.Domain.Exceptions;

namespace Ambev.DeveloperEvaluation.Common.Exceptions;

public class NotFoundException : DomainException
{
    private const string Default = "The requested resource was not found.";

    public NotFoundException() : base(Default)
    {
    }
    
    public NotFoundException(string message) : base(message)
    {
    }

    public NotFoundException(string message, Exception innerException) : base(message, innerException)
    {
    }
}