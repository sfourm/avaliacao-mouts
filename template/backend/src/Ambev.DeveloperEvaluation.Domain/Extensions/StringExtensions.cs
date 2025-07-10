namespace Ambev.DeveloperEvaluation.Domain.Extensions;

public static class StringExtensions
{
    public static bool IsEmpty(this string? value)
    {
        return string.IsNullOrWhiteSpace(value);
    }
}