using System.Text.Json;

namespace Ambev.DeveloperEvaluation.Packages.Application.Extensions;

internal static class JsonExtensions
{
    private static JsonSerializerOptions SerializerOptions => new() { PropertyNameCaseInsensitive = true };

    internal static string ToJson(this object value)
    {
        return JsonSerializer.Serialize(value);
    }

    internal static T? FromJson<T>(this string value) where T : class
    {
        return JsonSerializer.Deserialize<T>(value, SerializerOptions);
    }
}