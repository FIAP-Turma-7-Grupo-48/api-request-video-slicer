using Api.Request.Video.Slicer.Domain.Enum;

namespace Api.Request.Video.Slicer.Controller.Dtos
{
    public record ListVideoRequestResponse
    {
        public string Id { get; init; } = string.Empty;
        public string FileName { get; init; } = string.Empty;
        public string Extension { get; init; } = string.Empty;
        public RequestStatus Status { get; set; } 

    }
}
