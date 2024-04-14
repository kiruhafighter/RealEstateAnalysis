using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
        
        return services;
    }
}