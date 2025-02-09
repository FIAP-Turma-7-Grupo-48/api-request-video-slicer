using Amazon.S3.Model.Internal.MarshallTransformations;
using Api.Request.Video.Slicer.Controller.Application.Interfaces;
using Api.Request.Video.Slicer.Domain;
using Api.Request.Video.Slicer.Domain.Entities.Dtos.VideoRequestResponse;
using Api.Request.Video.Slicer.UseCase.Dtos;
using Api.Request.Video.Slicer.UseCase.UseCase.Interfaces;

namespace Api.Request.Video.Slicer.controller.Application;

public class VideoRequestApplication : IVideoRequestApplication
{
    private readonly IVideoRequestUseCase _videoRequestUseCase;
    public VideoRequestApplication(IVideoRequestUseCase VideoRequestUseCase)
    {
        _videoRequestUseCase = VideoRequestUseCase;
    }

    public async Task<CreateVideoRequestResponse?> CreateAsync(CreateVideoRequestRequest createVideoRequestRequest)
    {

        var VideoRequest = await _videoRequestUseCase.CreateAsync(createVideoRequestRequest);

        return new CreateVideoRequestResponse() { videoRequestId = VideoRequest.Id };
    }

    public Task UpdateAsync(UpdateVideoRequestStatus updateVideoRequestStatus)
    {
        return _videoRequestUseCase.UpdateStatusAsync(updateVideoRequestStatus);
    }

    public Task<CreateVideoRequestResponse?> CreateInBucket(CreateVideoRequestRequest createVideoRequestRequest, Stream stream)
    {
        throw new NotImplementedException();
    }

    public async Task<GetImagesResponse?> GetById(string id)
    {
        return await _videoRequestUseCase.GetById(id);
    }
}
