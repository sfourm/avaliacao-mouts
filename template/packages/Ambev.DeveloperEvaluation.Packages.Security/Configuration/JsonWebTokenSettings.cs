using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Ambev.DeveloperEvaluation.Packages.Security.Configuration;

public class JsonWebTokenSettings
{
    private readonly IDictionary<string, Action<JwtBearerOptions>> _jsonWebTokenBearerOptions;
    private string _authority = string.Empty;
    private string _key = string.Empty;

    public JsonWebTokenSettings()
    {
        _jsonWebTokenBearerOptions = new Dictionary<string, Action<JwtBearerOptions>>();
    }

    public string Key
    {
        get => _key;
        set
        {
            _key = value;

            if (string.IsNullOrWhiteSpace(_key)) return;

            _jsonWebTokenBearerOptions.Add(nameof(Key), options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key))
                };
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
            });
        }
    }

    public string Authority
    {
        get => _authority;
        set
        {
            _authority = value;

            if (string.IsNullOrWhiteSpace(_authority)) return;

            _jsonWebTokenBearerOptions.Add(nameof(Authority), options =>
            {
                options.Authority = _authority;
                options.TokenValidationParameters = new TokenValidationParameters { ValidateAudience = false };
            });
        }
    }

    public AuthenticationBuilder BuildJsonWebTokenOptions(AuthenticationBuilder builder)
    {
        return _jsonWebTokenBearerOptions.Count switch
        {
            0 => throw new InvalidOperationException("Json Web Token Settings it is not configured"),
            > 1 => throw new InvalidOperationException("You must choose between Authority and Signing Key"),
            _ => builder.AddJwtBearer(options => _jsonWebTokenBearerOptions.First().Value.Invoke(options))
        };
    }
}