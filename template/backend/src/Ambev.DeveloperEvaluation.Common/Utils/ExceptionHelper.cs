namespace Ambev.DeveloperEvaluation.Common.Utils;

public static class ExceptionHelper
{
    public static string StringMessage(string message, string defaultMessage)
    {
        return string.IsNullOrEmpty(message) ? defaultMessage : message;
    }
}