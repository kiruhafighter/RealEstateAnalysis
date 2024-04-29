using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services.Implementations;
using Services.IServices;
using Services.Utils;

namespace Services;

public static class BLLServicesRegistration
{
    public static IServiceCollection AddBusinessLogicServices(this IServiceCollection services,
        IConfiguration configuration)
    {
            services.Configure<JWTOptions>(configuration.GetSection("Jwt"));

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAgentService, AgentService>();
            services.AddScoped<IPropertyService, PropertyService>();
            services.AddScoped<IImageService, ImageService>();
            services.AddScoped<IOfferService, OfferService>();
            services.AddScoped<IUsersFavouriteService, UsersFavouriteService>();

            return services;
        }
}