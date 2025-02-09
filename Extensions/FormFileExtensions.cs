using Api.Request.Video.Slicer.Domain.ValueObjects;

namespace api_request_video_slicer.Extensions
{
    public static class FormFileExtensions
    {
        public static async Task<VideoFile> ToVideoFileAsync(this IFormFile file, CancellationToken cancellationToken)
        {
            byte[] fileBytes;
            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream, cancellationToken);
                fileBytes = memoryStream.ToArray();
            }

            VideoFile photo = new VideoFile(file.FileName, file.ContentType, fileBytes);

            return photo;
        }
    }
}
