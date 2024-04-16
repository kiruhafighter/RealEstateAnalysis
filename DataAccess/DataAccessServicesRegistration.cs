using DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repositories;

namespace DataAccess;
    
public static class DataAccessServicesRegistration
{
    private const string DBConnectionStringKey = "RealEstateManagementDB";
    
    public static IServiceCollection AddDataAccessServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<DBContext>((serviceProvider, builder) =>
        {
            builder.UseSqlServer(configuration.GetConnectionString(DBConnectionStringKey));
        });
        
        services.AddScoped<IAgentRepository, AgentRepository>();
        services.AddScoped<IImageRepository, ImageRepository>();
        services.AddScoped<IOfferRepository, OfferRepository>();
        services.AddScoped<IPropertyRepository, PropertyRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUsersFavouriteRepository, UsersFavouriteRepository>();
        
        return services;
    }
}