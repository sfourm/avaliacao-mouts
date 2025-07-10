namespace Ambev.DeveloperEvaluation.Packages.Application.Utils;

public static class FluentValidationMethods
{
    public static DateTime InvalidDateTime => DateTime.Parse("11/11/1111 11:11:11");

    public static bool ValidateDateTimeIso8601(DateTime dateTime)
    {
        return dateTime != InvalidDateTime;
    }

    public static string MessageForDate()
    {
        return $"The date is in the wrong format. It must be in the ISO 8601 format. Ex.: {DateTime.Now.ToString("O")}";
    }
}