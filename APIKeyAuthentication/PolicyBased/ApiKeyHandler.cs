using APIKeyAuthentication.Interface;
using Microsoft.AspNetCore.Authorization;

namespace APIKeyAuthentication.PolicyBased;

public class ApiKeyHandler : AuthorizationHandler<ApiKeyRequirement>
{
    private readonly IHttpContextAccessor _context;
    private readonly IApiKeyValidation _apiKeyValidation;

    public ApiKeyHandler(IHttpContextAccessor context, IApiKeyValidation apiKeyValidation)
    {
        _context = context;
        _apiKeyValidation = apiKeyValidation;
    }

    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ApiKeyRequirement requirement)
    {
        string? userApiKey = _context?.HttpContext?.Request.Headers[Constants.ApiKeyHeaderName];
        if (string.IsNullOrWhiteSpace(userApiKey))
        {
            context.Fail();
            return Task.CompletedTask;
        }
        if (!_apiKeyValidation.IsValidApiKey(userApiKey))
        {
            context.Fail();
            return Task.CompletedTask;
        }
        context.Succeed(requirement);
        return Task.CompletedTask;
    }
}
