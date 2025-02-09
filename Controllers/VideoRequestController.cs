using Api.Request.Video.Slicer.Controller.Application.Interfaces;
using Api.Request.Video.Slicer.Domain.Entities.Dtos.VideoRequestResponse;
using Api.Request.Video.Slicer.Domain.Enum;
using Api.Request.Video.Slicer.UseCase.Dtos;
using api_request_video_slicer.Extensions;
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
        public async Task<IActionResult> UploadVideo(IFormFile formFile, CancellationToken cancellationToken)
        {
            if (formFile == null || formFile.Length == 0)
            {
                return BadRequest("Arquivo vazio ou inexistente");
            }

            if (!formFile.ContentType.StartsWith("video/"))
            {
                return BadRequest("formato inválido, por favor envie um arquivo do tipo video.");
            }

            try
            {
                var videoFile = await formFile.ToVideoFileAsync(cancellationToken);

                CreateVideoRequestRequest createVideo = new()
                {
                    Video = videoFile
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
