using Api.Request.Video.Slicer.Controller.Application.Dtos.VideoRequestResponse;
using Api.Request.Video.Slicer.UseCase.Dtos;

namespace Api.Request.Video.Slicer.Controller.Application.Interfaces;

public interface IVideoRequestApplication
{
	Task<GetVideoRequestResponse?> GetById(string id, CancellationToken cancellationToken);
	Task<CreateVideoRequestResponse?> CreateAsync(CreateVideoRequestRequest createVideoRequestRequest, CancellationToken cancellationToken);
}
