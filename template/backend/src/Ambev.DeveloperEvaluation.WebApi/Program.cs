using Ambev.DeveloperEvaluation.Application;
using Ambev.DeveloperEvaluation.Infrastructure;
using Ambev.DeveloperEvaluation.Packages.Security;
using Ambev.DeveloperEvaluation.Packages.WebApi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
var environment = builder.Environment;

builder.Services.RegisterApplicationDependencies();
builder.Services.RegisterInfrastructureDependencies(configuration, environment);
builder.Services.RegisterApplicationAuthentication(configuration);
builder.Services.RegisterApplicationAuthorization(options =>
{
    options.DefaultPolicy = new AuthorizationPolicyBuilder().Build();
});


builder.Services.RegisterApplicationApiServices(configuration);

builder.Services.AddHealthChecks()
    .RegisterApplicationApiObservability()
    .AddNpgSql(configuration.GetConnectionString("DefaultConnection")!);

var app = builder.Build();

var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
app.ConfigureApplicationApiMiddleware(provider);

app.Run();