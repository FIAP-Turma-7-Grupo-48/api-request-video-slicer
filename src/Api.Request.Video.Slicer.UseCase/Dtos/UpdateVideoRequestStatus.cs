using Api.Request.Video.Slicer.Domain.Enum;
using Api.Request.Video.Slicer.Domain.ValueObjects;

namespace Api.Request.Video.Slicer.UseCase.Dtos;

public class UpdateVideoRequestStatus
{
    public string RequestId { get; init; } = string.Empty;
    public RequestStatus Status { get; init; }
    public StorageFile File { get; init; }
}
