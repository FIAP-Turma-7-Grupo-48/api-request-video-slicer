using Api.Request.Video.Slicer.Domain.Entities.Dtos.VideoRequestResponse;
using Api.Request.Video.Slicer.Domain;

namespace api_request_video_slicer.Extensions
{
    public static class ResponseExtension
    {
        public static CreateVideoRequestResponse ToCreateVideoRequestResponse(this VideoRequest videoRequest)
        {
            CreateVideoRequestResponse videoRequestResponse = new();
            videoRequestResponse.videoRequestId = videoRequest.id;
            return videoRequestResponse;
        }
    }
}
