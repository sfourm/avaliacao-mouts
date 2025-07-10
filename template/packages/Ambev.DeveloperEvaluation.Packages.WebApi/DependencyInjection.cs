using System.Text.Json;
using System.Text.Json.Serialization;
using Ambev.DeveloperEvaluation.Packages.Application.Extensions;
using Ambev.DeveloperEvaluation.Packages.Security;
using Ambev.DeveloperEvaluation.Packages.WebApi.Configuration;
using Ambev.DeveloperEvaluation.Packages.WebApi.Converters;
using Ambev.DeveloperEvaluation.Packages.WebApi.Documentation;
using Ambev.DeveloperEvaluation.Packages.WebApi.Filters;
using Ambev.DeveloperEvaluation.Packages.WebApi.Observability;
using AspNetCore.Scalar;
using FluentValidation.AspNetCore;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Ambev.DeveloperEvaluation.Packages.WebApi;

public static class DependencyInjection
{
    public static IServiceCollection RegisterApplicationApiServices(this IServiceCollection services,
        IConfiguration configuration, Action<JsonSerializerOptions>? jsonSerializerOptions = null)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(configuration);

        services
            .AddOptions()
            .Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);
        var settings = configuration.GetRequiredSection(nameof(WebApiSettings)).Get<WebApiSettings>()!;
        services.AddSingleton(settings);

        services
            .AddEndpointsApiExplorer()
            .AddRouting(options => options.LowercaseUrls = true);

        services.AddFluentValidationClientsideAdapters();
        services.AddFluentValidationAutoValidation();
        services.AddDefaultExceptionHandlers();

        services
            .AddControllers(options =>
            {
                //Exception Filter
                options.Filters.Add<ApiExceptionFilterAttribute>();
            })
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                options.JsonSerializerOptions.Converters.Add(new JsonTrimStringConverter());
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                jsonSerializerOptions?.Invoke(options.JsonSerializerOptions);
            });

        services
            .AddApiVersioning(setup =>
            {
                setup.DefaultApiVersion = new ApiVersion(1, 0);
                setup.AssumeDefaultVersionWhenUnspecified = true;
                setup.ReportApiVersions = true;
            })
            .AddVersionedApiExplorer(setup =>
            {
                setup.GroupNameFormat = "'v'VVV";
                setup.SubstituteApiVersionInUrl = true;
            });

        services
            .AddSwaggerGen()
            .ConfigureOptions<ConfigureSwaggerOptions>();

        if (settings.EnableSwaggerFluentValidation) services.AddFluentValidationRulesToSwagger();

        return services;
    }

    public static IHealthChecksBuilder RegisterApplicationApiObservability(
        this IHealthChecksBuilder builder, string name = "Readiness")
    {
        return builder.AddCheck<SimpleReadinessHealthCheck>(name);
    }

    public static IApplicationBuilder ConfigureApplicationApiMiddleware(this IApplicationBuilder builder,
        IApiVersionDescriptionProvider provider)
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(provider);

        if (EnvironmentExtensions.IsDevelopment())
            builder.UseCors(c => c.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

        var webApiSettings = builder.ApplicationServices.GetRequiredService<WebApiSettings>();
        builder.ConfigureBasePath(webApiSettings);

        if (webApiSettings.UseHttpsRedirection) builder.UseHttpsRedirection();

        builder.UseRouting();
        builder.UseExceptionHandler("/error");
        builder.ConfigureApplicationSecurity();
        builder.UseForwardedHeaders(new ForwardedHeadersOptions
        {
            ForwardedHeaders = ForwardedHeaders.XForwardedProto
        });
        builder.MapDefaultEndpoints();
        builder.UseSwagger(provider, webApiSettings);

        if (webApiSettings.EnableScalar)
            builder.UseScalar(options =>
            {
                options.UseTheme(Theme.Default);
                options.RoutePrefix = "scalar";
                options.DocumentTitle = webApiSettings.DisplayName;
            });

        return builder;
    }

    private static IApplicationBuilder MapDefaultEndpoints(this IApplicationBuilder builder)
    {
        return builder.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapHealthChecks("/health/liveness").ShortCircuit();
            endpoints.MapHealthChecks("/health/readiness",
                new HealthCheckOptions { Predicate = check => check.Name == "Readiness" }).ShortCircuit();
        });
    }

    private static IApplicationBuilder UseSwagger(this IApplicationBuilder builder,
        IApiVersionDescriptionProvider provider, WebApiSettings webApiSettings)
    {
        builder.UseSwagger(options =>
        {
            if (!string.IsNullOrWhiteSpace(webApiSettings.BasePath))
                options.PreSerializeFilters.Add((document, _) =>
                    document.Servers = [new OpenApiServer { Url = webApiSettings.BasePath }]);
        });

        builder.UseSwaggerUI(options =>
            provider.ApiVersionDescriptions.Select(description => description.GroupName).ToList().ForEach(version =>
                options.SwaggerEndpoint($"{webApiSettings.BasePath}/swagger/{version}/swagger.json",
                    $"{webApiSettings.DisplayName} {version.ToUpper()}")));

        return builder;
    }

    private static IApplicationBuilder ConfigureBasePath(this IApplicationBuilder builder,
        WebApiSettings webApiSettings)
    {
        if (!string.IsNullOrWhiteSpace(webApiSettings.BasePath))
            builder.UsePathBase(new PathString(webApiSettings.BasePath));

        return builder;
    }
}