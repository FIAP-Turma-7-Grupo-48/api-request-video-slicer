using Api.Request.Video.Slicer.Controller.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
        [HttpPost]
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
                string uploadPath = Path.Combine(System.Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), "uploads");

                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }

                string fileName = Path.GetFileName(videoFile.FileName);
                string fileWithPath = Path.Combine(uploadPath, fileName);

                using (var stream = new FileStream(fileWithPath, FileMode.Create))
                {
                    await videoFile.CopyToAsync(stream);
                }

                //CreateVideoRequestRequest createVideo = new()
                //{
                    
                //};

                //_videoRequestApplication.CreateAsync();

                return Ok($"Arquivo {fileName} enviado com sucesso!");
            }
            catch (Exception ex)
            {
                return Problem($"Houve um erro no envio do video: {ex.Message}");
            }

        }
    }
}
