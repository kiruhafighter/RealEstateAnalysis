using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using NJsonSchema;
using NJsonSchema.Generation;
using NSwag;
using NSwag.Generation.Processors.Security;

namespace RealEstateAnalysis.Utils;

public static class SwaggerExtensions
{
    public static IServiceCollection AddApiSwaggerDocument(this IServiceCollection services,
        IConfiguration configuration)
    {
         var title = configuration["Swagger:Title"];
         var scheme = configuration["Swagger:Scheme"];

         services.AddOpenApiDocument((config, serviceProvider) =>
         {
             var jsonOptions = serviceProvider.GetRequiredService<IOptions<JsonOptions>>();
             config.SchemaSettings = new SystemTextJsonSchemaGeneratorSettings
             {
                 SchemaType = SchemaType.OpenApi3,
                 SerializerOptions = jsonOptions.Value.SerializerOptions
             };
             
             config.DocumentName = "v1";
             config.Title = title;
             config.Version = "v1";
             
             config.AddSecurity(scheme, Enumerable.Empty<string>(), new OpenApiSecurityScheme
             {
                 Type = OpenApiSecuritySchemeType.ApiKey,
                 In = OpenApiSecurityApiKeyLocation.Header,
                 Name = HeaderNames.Authorization,
                 Scheme = JwtBearerDefaults.AuthenticationScheme,
                 BearerFormat = "JWT",
                 Description = "Copy 'Bearer ' + valid JWT token into field"
             });
             
             config.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor(scheme));
         });

         return services;
    }
}