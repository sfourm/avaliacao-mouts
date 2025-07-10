using Ambev.DeveloperEvaluation.Packages.WebApi.Configuration;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Ambev.DeveloperEvaluation.Packages.WebApi.Documentation;

public class ConfigureSwaggerOptions : IConfigureNamedOptions<SwaggerGenOptions>
{
    private readonly IApiVersionDescriptionProvider _provider;
    private readonly WebApiSettings _webApiSettings;

    public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider, WebApiSettings settings)
    {
        ArgumentNullException.ThrowIfNull(provider);

        _provider = provider;
        _webApiSettings = settings;
    }

    public void Configure(string? name, SwaggerGenOptions options)
    {
        Configure(options);
        options.CustomSchemaIds(SwashbuckleSchemaHelper.GetSchemaId);
    }

    public void Configure(SwaggerGenOptions options)
    {
        options.SupportNonNullableReferenceTypes();
        ConfigureVersionInfo(options);
        ConfigureSecurity(options);
        ConfigureComments(options);
        AddServers(options);
        options.EnableAnnotations();
    }

    private void AddServers(SwaggerGenOptions options)
    {
        foreach (var server in _webApiSettings.Servers)
            options.AddServer(new OpenApiServer { Description = server.Description, Url = server.Url });
    }

    private void ConfigureVersionInfo(SwaggerGenOptions options)
    {
        foreach (var description in _provider.ApiVersionDescriptions)
            options.SwaggerDoc(description.GroupName, CreateVersionInfo(description));
    }

    private void ConfigureComments(SwaggerGenOptions options)
    {
        IEnumerable<string> comments =
        [
            Path.Combine(AppContext.BaseDirectory,
                $"{(string.IsNullOrEmpty(_webApiSettings.ProjectName) ? _webApiSettings.DisplayName : _webApiSettings.ProjectName)}.WebApi.xml"),
            Path.Combine(AppContext.BaseDirectory,
                $"{(string.IsNullOrEmpty(_webApiSettings.ProjectName) ? _webApiSettings.DisplayName : _webApiSettings.ProjectName)}.Application.xml")
        ];

        comments.Where(File.Exists).ToList().ForEach(comment =>
            options.IncludeXmlComments(comment, true));
    }

    private void ConfigureSecurity(SwaggerGenOptions options)
    {
        if (_webApiSettings.EnableBearerTokenSecurityDefinition is false) return;

        options.AddSecurityDefinition("Bearer",
            new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Description = "Please enter the word 'Bearer' followed by a space and then your JWT.",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });

        options.AddSecurityRequirement(
            new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header
                    },
                    Array.Empty<string>()
                }
            });
    }

    private OpenApiInfo CreateVersionInfo(ApiVersionDescription description)
    {
        OpenApiInfo info = new() { Title = _webApiSettings.DisplayName, Version = description.ApiVersion.ToString() };

        if (description.IsDeprecated) info.Description += " This API version has been deprecated.";

        return info;
    }
}