using APIKeyAuthentication.Auth;
using APIKeyAuthentication.Filters.EndpointFilter;
using APIKeyAuthentication.Interface;
using APIKeyAuthentication.PolicyBased;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer();
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ApiKeyPolicy", policy =>
    {
        policy.AddAuthenticationSchemes(new[] { JwtBearerDefaults.AuthenticationScheme });
        policy.Requirements.Add(new ApiKeyRequirement());
    });
});
builder.Services.AddScoped<IAuthorizationHandler, ApiKeyHandler>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IApiKeyValidation, ApiKeyValidation>();
builder.Services.AddScoped<APIKeyAuthFilter>();
builder.Services.AddHttpContextAccessor();

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//app.UseMiddleware<ApiKeyMiddleware>(); //web api auth using middleware
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapGet("api/product", () =>
{
    return Results.Ok("products");
}).AddEndpointFilter<ApiKeyEndpointFilter>();
app.Run();
