
using APIKeyAuthentication.Interface;

namespace APIKeyAuthentication.Filters.EndpointFilter;

public class ApiKeyEndpointFilter : IEndpointFilter
{
    private readonly IApiKeyValidation _apiKeyValidation;

    public ApiKeyEndpointFilter(IApiKeyValidation apiKeyValidation)
    {
        _apiKeyValidation = apiKeyValidation;
    }

    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        string? userApiKey = context.HttpContext.Request.Headers[Constants.ApiKeyHeaderName];
        if (string.IsNullOrWhiteSpace(userApiKey))
        {

            return Results.BadRequest();
        }
        if (!_apiKeyValidation.IsValidApiKey(userApiKey))
        {
            return Results.Unauthorized();
        }
        return await next(context);
    }
}
