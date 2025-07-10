namespace Ambev.DeveloperEvaluation.Packages.WebApi.Documentation;

internal static class SwashbuckleSchemaHelper
{
    private static readonly Dictionary<string, int> SchemaNameRepetition = new();

    public static string GetSchemaId(Type type)
    {
        var id = type.Name;

        if (!SchemaNameRepetition.ContainsKey(id)) SchemaNameRepetition.Add(id, 0);

        var count = SchemaNameRepetition[id] + 1;
        SchemaNameRepetition[id] = count;

        return type.Name + (count > 1 ? count.ToString() : "");
    }
}