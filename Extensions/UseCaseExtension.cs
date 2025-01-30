using Api.Request.Video.Slicer.UseCase.UseCase.Interfaces;
using Api.Request.Video.Slicer.UseCase.UseCase;

namespace WebApi.Extensions;

public static class UseCaseExtension
{
	public static IServiceCollection AddUseCase(this IServiceCollection services)
	{
		return
			services
				.AddServices();
	}

	private static IServiceCollection AddServices(this IServiceCollection services)
	{
		return services
			.AddScoped<IVideoRequestUseCase, VideoRequestUseCase>();			
	}	
}