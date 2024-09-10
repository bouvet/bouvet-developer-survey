using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Bouvet.Developer.Survey.Api.Swagger;

public class SwaggerUIConfigurator : IConfigureOptions<SwaggerUIOptions>
{
    private readonly IApiVersionDescriptionProvider _apiVersionProvider;
    private readonly IConfiguration _config;

    public SwaggerUIConfigurator(IApiVersionDescriptionProvider apiVersionProvider, IConfiguration config)
    {
        _apiVersionProvider = apiVersionProvider;
        _config = config;
    }

    public void Configure(SwaggerUIOptions options)
    {
        foreach (var description in _apiVersionProvider.ApiVersionDescriptions)
        {
            options.SwaggerEndpoint(
                $"/swagger/{description.GroupName}/swagger.json",
                $"Bouvet.Developer.Survey.Api {description.ApiVersion}"
            );
        }

        options.OAuthAppName(_config["Swagger:AppName"]);
        options.OAuthClientId(_config["Swagger:ClientId"]);
        options.OAuthScopes($"api://{_config["AzureAd:ClientId"]}/user_impersonation");
        options.OAuthUsePkce();
        options.RoutePrefix = string.Empty;
    }
}