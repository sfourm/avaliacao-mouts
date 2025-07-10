using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Ambev.DeveloperEvaluation.Packages.Application.Utils;

public class DateTimeJsonConverter : JsonConverter<DateTime>
{
    public override DateTime Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options)
    {
        try
        {
            return DateTime.ParseExact(reader.GetString()!, "O", CultureInfo.InvariantCulture);
        }
        catch
        {
            return FluentValidationMethods.InvalidDateTime;
        }
    }

    public override void Write(
        Utf8JsonWriter writer,
        DateTime dateTimeValue,
        JsonSerializerOptions options)
    {
        writer.WriteStringValue(dateTimeValue.ToString("O"));
    }
}