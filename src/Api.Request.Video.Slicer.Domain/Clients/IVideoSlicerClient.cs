
namespace Api.Request.Video.Slicer.Domain
{
    public interface IVideoSlicerClient
    {
        Task SendAsync(VideoRequest videoRequest);
    }
}
