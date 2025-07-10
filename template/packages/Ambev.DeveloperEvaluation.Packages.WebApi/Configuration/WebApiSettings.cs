using System.Reflection;

namespace Ambev.DeveloperEvaluation.Packages.WebApi.Configuration;

public class WebApiSettings
{
    private string _basePath = string.Empty;

    private string _displayName = Assembly.GetEntryAssembly()!.GetName().Name!;

    private string? _projectName;

    public string BasePath
    {
        get => _basePath.SanitizeBasePath();
        set => _basePath = value;
    }

    public string DisplayName
    {
        get => _displayName;
        set => _displayName = string.IsNullOrWhiteSpace(value) ? _displayName : value;
    }

    public string? ProjectName
    {
        get => _projectName;
        set => _projectName = string.IsNullOrWhiteSpace(value) ? _projectName : value;
    }

    /// <summary>
    ///     Flag to toggle the display of the 'Authorize' section in the generated Swagger documentation.
    /// </summary>
    public bool EnableBearerTokenSecurityDefinition { get; set; } = true;

    /// <summary>
    ///     Flag to toggle the display of the 'X-ME-CORRELATION-ID' header parameter in the generated Swagger documentation.
    /// </summary>
    public bool EnableCorrelationTokenParameter { get; set; } = false;

    /// <summary>
    ///     Provide specific server information to include in the generated Swagger document.
    /// </summary>
    public List<Server> Servers { get; set; } = [];

    /// <summary>
    ///     Enables the use of HTTPS redirection middleware.
    /// </summary>
    public bool UseHttpsRedirection { get; set; } = true;

    /// <summary>
    ///     Enables th use of Scalar
    /// </summary>
    public bool EnableScalar { get; set; } = true;
}

public class Server
{
    /// <summary>
    ///     A URL to the target host
    /// </summary>
    public string Url { get; set; } = string.Empty;

    /// <summary>
    ///     An optional string describing the host designated by the URL. CommonMark syntax MAY be used for rich text
    ///     representation.
    /// </summary>
    public string Description { get; set; } = string.Empty;
}