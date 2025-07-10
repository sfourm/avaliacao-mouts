namespace Ambev.DeveloperEvaluation.Packages.Application.Utils;

public static class ExceptionHelper
{
    public static string StringMessage(string message, string defaultMessage)
    {
        return string.IsNullOrEmpty(message) ? defaultMessage : message;
    }
}