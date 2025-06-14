﻿using APIKeyAuthentication.Interface;
namespace APIKeyAuthentication.Auth;

public class ApiKeyValidation : IApiKeyValidation
{
    private readonly IConfiguration _configuration;
    public ApiKeyValidation(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public bool IsValidApiKey(string userApiKey)
    {
        if (string.IsNullOrEmpty(userApiKey))
            return false;
        var apiKey = _configuration.GetValue<string>(Constants.ApiKeyName);
        if (apiKey is null || apiKey != userApiKey)
            return false;
        return true;

    }
}
