using System.Reflection;

namespace Ambev.DeveloperEvaluation.Packages.WebApi.Configuration;

public static class ConfigurationExtensions
{
    public static string SanitizeBasePath(this string rawBasePathInput)
    {
        return !string.IsNullOrEmpty(rawBasePathInput) && !rawBasePathInput.StartsWith('/')
            ? rawBasePathInput.Insert(0, "/")
            : rawBasePathInput;
    }

    public static string SanitizeDisplayName(this string displayName)
    {
        return string.IsNullOrWhiteSpace(displayName)
            ? Assembly.GetExecutingAssembly()
                .GetTemplateType()
                .GetTemplateName()
                .EnsureTemplateNameEndsWithDescriptor()
                .Trim()
            : displayName;
    }

    private static string EnsureTemplateNameEndsWithDescriptor(this string templateName, string descriptor = "API")
    {
        return templateName.EndsWith(descriptor, StringComparison.OrdinalIgnoreCase)
            ? templateName
            : templateName + " " + descriptor;
    }

    private static string GetTemplateName(this Type type)
    {
        return type.Namespace?[..type.Namespace.IndexOf('.')] ?? string.Empty;
    }

    private static Type GetTemplateType(this Assembly assembly)
    {
        return assembly.GetTypes().First(f => f.Name.Contains("Program", StringComparison.OrdinalIgnoreCase));
    }
}