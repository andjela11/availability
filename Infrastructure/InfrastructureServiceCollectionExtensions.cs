using Application.Interfaces;
using Infrastructure.Helpers;
using Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class InfrastructureServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClient<IMovieHttpClient, MovieHttpClient>();
        services.AddHttpClient<IReservationHttpClient, ReservationHttpClient>();

        services.Configure<MongoDBSettings>(configuration.GetSection("MongoDB"));
        services.AddSingleton<IMongoDBService, MongoDBService>();
        return services;
    }
}
