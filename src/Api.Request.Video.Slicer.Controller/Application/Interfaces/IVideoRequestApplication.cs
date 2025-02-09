using Api.Request.Video.Slicer.Domain.Entities.Dtos.VideoRequestResponse;
using Api.Request.Video.Slicer.UseCase.Dtos;

namespace Api.Request.Video.Slicer.Controller.Application.Interfaces;

public interface IVideoRequestApplication
{
	Task<GetImagesResponse?> GetById(string id);
	Task<CreateVideoRequestResponse?> CreateAsync(CreateVideoRequestRequest createVideoRequestRequest);    
}
