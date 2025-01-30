namespace Api.Request.Video.Slicer.Controller.Application.Dtos.VideoRequestResponse;

public record GetVideoRequestResponse
{
    public int Id { get; init; }
    public string fileName { get; init; } = string.Empty;    
}
