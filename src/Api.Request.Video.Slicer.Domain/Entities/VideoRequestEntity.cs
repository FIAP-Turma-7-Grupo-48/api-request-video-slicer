using Api.Request.Video.Slicer.Domain.Enum;
using Api.Request.Video.Slicer.Domain.ValueObjects;


namespace Api.Request.Video.Slicer.Domain
{
    public class VideoRequest
    {
        public VideoRequest()
        {
            Id = Guid.NewGuid().ToString();
        }
        public string Id { get; init; } 
        public string FileName { get; init; } = string.Empty;
        public string Extension { get; init; } = string.Empty;
        public RequestStatus Status { get; set; } = RequestStatus.Receveid;
        public StorageFile Video { get; set; }
        public StorageFile? ZippedImg { get; set; }
    }

}
