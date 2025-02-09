namespace Api.Request.Video.Slicer.Domain.Entities.Dtos.VideoRequestResponse;

public record GetImagesResponse
{
    public byte[] Images { get; set; }
    public string FileName { get; set; }
}
