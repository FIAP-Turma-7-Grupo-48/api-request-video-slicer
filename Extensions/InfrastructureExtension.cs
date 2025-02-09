using Api.Request.Video.Slicer.Domain;
using Infrastructure.Clients.RabbbitMq;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using Api.Request.Video.Slicer.Infrastructure.Context;
using Api.Request.Video.Slicer.Infrastucture.Repositories.interfaces;
using Api.Request.Video.Slicer.Infrastucture.Repositories;
using Api.Request.Video.Slicer.Infrastucture.Context;
using Api.Request.Video.Slicer.Infrastructure.Repository.Interfaces;
using Api.Request.Video.Slicer.Infrastructure.Repository;

namespace api.request.video.Extensions;

internal static class InfrastructureExtension
{
	private static string ConnectionString;

	static InfrastructureExtension()
	{
		ConnectionString = GetConnectionString();
	}
	public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
	{

		return services
            .AddContext(configuration)
			.AddS3FileStorage()
            .AddMongoRepositories()			
			.AddClients(configuration)			
			.AddRabbitMqConnectionFactory(configuration);
	}

	private static IServiceCollection AddMongoRepositories(this IServiceCollection services)
	{
		return services
			.AddScoped<IVideoRequestRepository, VideoRequestRepository>();		
	}
    private static IServiceCollection AddS3FileStorage(this IServiceCollection services)
    {
        return services
            .AddScoped<IFileStorageRepository, S3FileStorageRepository>();
    }

    private static IServiceCollection AddContext(this IServiceCollection services, IConfiguration configuration)
	{	
        var mongoConfig = new MongoDbConfig
        {
            ConnectionString = Environment.GetEnvironmentVariable("DefaultConnection"), 
            DatabaseName = Environment.GetEnvironmentVariable("DefaultDatabase"), 
            CollectionNames = new List<string> { "VideoRequest" }
        };

        services.AddSingleton(mongoConfig);
        services.AddSingleton<VideoSlicerDbContext>();

        return
            services
				.AddSingleton<VideoSlicerDbContext>();				
	}

    private static IServiceCollection AddClients(this IServiceCollection services, IConfiguration configuration)
    {
		return
			services
				.AddSingleton<IVideoSlicerClient, VideoSlicerRabbitMqClient>();
    }

    private static IServiceCollection AddRabbitMqConnectionFactory(this IServiceCollection services, IConfiguration configuration)
    {
        return
            services
                .AddSingleton<IConnectionFactory>(
					new ConnectionFactory() 
					{ 
						HostName = "localhost",
						Port = 5672,
                        UserName = Environment.GetEnvironmentVariable("RABBITMQ_USER"),
                        Password = Environment.GetEnvironmentVariable("RABBITMQ_PASS")
                    }
				);
    } 

	private static string GetConnectionString()
	{
		var connectionString = Environment.GetEnvironmentVariable("DefaultConnection");

		if (!string.IsNullOrEmpty(connectionString))
		{
			return connectionString;
		}

		throw new Exception("Enviroment Variable DefaultConnection not found ");
	}

}