using Api.Request.Video.Slicer.Domain.ValueObjects;

namespace Api.Request.Video.Slicer.Infrastucture.Clients.Domain
{
    public class SendVideoRequest
    {
        public string RequestId { get; init; } = string.Empty;
        public StorageFile StorageFile { get; init; }
    }
}
