using Api.Request.Video.Slicer.Controller.Application.Interfaces;
using Api.Request.Video.Slicer.Domain.Entities.Dtos.VideoRequestResponse;
using Api.Request.Video.Slicer.Domain.Enum;
using Api.Request.Video.Slicer.UseCase.Dtos;
using Microsoft.AspNetCore.Mvc;
using WebApi.Extensions;

namespace api_request_video_slicer.Controllers
{    
    public class VideoRequestController : Controller
    {
        private readonly IVideoRequestApplication _videoRequestApplication;        
        public VideoRequestController(IVideoRequestApplication videoRequestApplication) 
        {
            _videoRequestApplication = videoRequestApplication;
        }
        [HttpGet("health-check")]
        public async Task<IActionResult> healthCheck()
        {
            return Ok();
        }

        [HttpPost("upload-video")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]        
        public async Task<IActionResult> UploadVideo(IFormFile videoFile)
        {
            if (videoFile == null || videoFile.Length == 0)
            {
                return BadRequest("Arquivo vazio ou inexistente");
            }

            if (!videoFile.ContentType.StartsWith("video/"))
            {
                return BadRequest("formato inválido, por favor envie um arquivo do tipo video.");
            }

            try
            {
                //No linux ele retornará a pasta do home usuario que estará rodando a applicação
                string uploadPath = Path.Combine(System.Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), "uploads");

                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }

                string fileName = Path.GetFileName(videoFile.FileName);
                string fileWithPath = Path.Combine(uploadPath, fileName);
                string extension = fileName.Split(".").Last();                
                
                Stream stream = Stream.Null;
                await videoFile.CopyToAsync(stream);


                CreateVideoRequestRequest createVideo = new()
                {
                    Extension = extension,
                    FileName = fileName,
                    FileType = VideoType.MP4.GetCurrentVideoType(extension),
                    Stream = stream,
                    Path = fileWithPath,
                    ContentType = "video/mp4"
                };                    

                CreateVideoRequestResponse? response = await _videoRequestApplication.CreateAsync(createVideo);
                 
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Problem($"Houve um erro no envio do video: {ex.Message}");
            }

        }


        [HttpGet("download-images/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DownloadImages([FromQuery] string id)
        {
            GetImagesResponse? response = await _videoRequestApplication.GetById(id);

            return File(response.Images, "application/zip");
        }
    }
}
