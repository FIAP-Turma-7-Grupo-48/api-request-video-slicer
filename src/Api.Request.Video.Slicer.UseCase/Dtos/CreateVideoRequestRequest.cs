using Api.Request.Video.Slicer.Domain.ValueObjects;

namespace Api.Request.Video.Slicer.UseCase.Dtos
{
    public class CreateVideoRequestRequest
    {
        public string Id = Guid.NewGuid().ToString();
        public VideoFile Video { get; set; }
    }
}
