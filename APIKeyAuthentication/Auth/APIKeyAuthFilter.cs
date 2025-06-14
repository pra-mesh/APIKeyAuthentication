using APIKeyAuthentication.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace APIKeyAuthentication.Auth;

public class APIKeyAuthFilter : IAuthorizationFilter
{
    private readonly IApiKeyValidation _apiKeyValidation;

    public APIKeyAuthFilter(IApiKeyValidation apiKeyValidation)
    {
        _apiKeyValidation = apiKeyValidation;
    }
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var userApiKey = context.HttpContext.Request.Headers[Constants.ApiKeyHeaderName];
        if (string.IsNullOrEmpty(userApiKey))
        {
            context.Result = new BadRequestResult();
            return;
        }
        if (!_apiKeyValidation.IsValidApiKey(userApiKey))
        {
            context.Result = new UnauthorizedResult();
        }
    }
}
