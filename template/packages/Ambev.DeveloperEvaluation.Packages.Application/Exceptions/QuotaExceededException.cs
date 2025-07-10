namespace Ambev.DeveloperEvaluation.Packages.Application.Exceptions;

public class QuotaExceededException : Exception
{
    public QuotaExceededException()
    {
    }

    public QuotaExceededException(string message) : base(message)
    {
    }
}