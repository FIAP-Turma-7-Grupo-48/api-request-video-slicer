

using Api.Request.Video.Slicer.controller.Application;
using Api.Request.Video.Slicer.Controller.Application.Interfaces;

namespace WebApi.Extensions;

public static class ControllerLayerExtension
{
	public static IServiceCollection AddControllerLayerDI(this IServiceCollection services)
	{
		return
			services
				.AddApplication();
	}

	private static IServiceCollection AddApplication(this IServiceCollection services)
	{
		return services
			.AddScoped<IVideoRequestApplication, VideoRequestApplication>();		

	}
}
