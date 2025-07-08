namespace Ambev.DeveloperEvaluation.Common.Extensions;

public static class ExceptionExtensions
{
    public static string GetName(this Exception ex)
    {
        return ex.GetType().Name;
    }
}