
using Api.Request.Video.Slicer.Controller.Application.Dtos.VideoRequestResponse;
using Api.Request.Video.Slicer.Controller.Application.Interfaces;
using Api.Request.Video.Slicer.UseCase.Dtos;
using Api.Request.Video.Slicer.UseCase.UseCase.Interfaces;

namespace Api.Request.Video.Slicer.controller.Application;

public class VideoRequestApplication : IVideoRequestApplication
{
	private readonly IVideoRequestUseCase _VideoRequestUseCase;
	public VideoRequestApplication(IVideoRequestUseCase VideoRequestUseCase)
	{
		_VideoRequestUseCase = VideoRequestUseCase;
	}

	public async Task<CreateVideoRequestResponse?> CreateAsync(CreateVideoRequestRequest createVideoRequestRequest, CancellationToken cancellationToken)
	{

		var VideoRequest = await _VideoRequestUseCase.CreateAsync(createVideoRequestRequest, cancellationToken);
		//return VideoRequest?.ToCreateVideoRequestResponse(); 
		return new CreateVideoRequestResponse();
	}

    public Task<GetVideoRequestResponse?> GetById(string id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
