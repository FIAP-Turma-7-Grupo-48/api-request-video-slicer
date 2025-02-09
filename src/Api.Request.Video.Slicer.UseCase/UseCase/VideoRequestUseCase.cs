using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Api.Request.Video.Slicer.Domain;
using Api.Request.Video.Slicer.Domain.Entities.Dtos.VideoRequestResponse;
using Api.Request.Video.Slicer.Infrastructure.Repository.Interfaces;
using Api.Request.Video.Slicer.Infrastucture.Repositories;
using Api.Request.Video.Slicer.Infrastucture.Repositories.interfaces;
using Api.Request.Video.Slicer.UseCase.Dtos;
using Api.Request.Video.Slicer.UseCase.UseCase.Interfaces;

namespace Api.Request.Video.Slicer.UseCase.UseCase
{
    public class VideoRequestUseCase : IVideoRequestUseCase
    {
        private readonly IVideoRequestRepository _videoRequestRepository;
        private readonly IFileStorageRepository _fileStorageRepository;
        private readonly IVideoSlicerClient _videoSlicerClient;
        public VideoRequestUseCase(
            IVideoRequestRepository videoRequestRepository,
            IFileStorageRepository fileStorageRepository,
            IVideoSlicerClient videoSlicerClient
        )
        { 
            _videoRequestRepository = videoRequestRepository;
            _fileStorageRepository = fileStorageRepository;
            _videoSlicerClient = videoSlicerClient;
        }
        public async Task<VideoRequest> CreateAsync(CreateVideoRequestRequest createVideoRequestRequest)
        {

            VideoRequest videoRequest = new();

            try
            {
                await _fileStorageRepository.UploadFileAsync(createVideoRequestRequest.Stream, $"{createVideoRequestRequest.FileName}-{videoRequest.id}");

                videoRequest.extension = createVideoRequestRequest.Extension;
                videoRequest.fileName = createVideoRequestRequest.FileName;
                videoRequest.videoTypeEnum = createVideoRequestRequest.FileType;
                videoRequest.videoUrl = $"{Environment.GetEnvironmentVariable("AWS_S3_BUCKET")}{createVideoRequestRequest.FileName}-{videoRequest.id}";

                await _videoRequestRepository.Create(videoRequest);

                await _videoSlicerClient.SendAsync(videoRequest);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }
                        
            return Task.FromResult(videoRequest).Result;
        }

        public async Task<GetImagesResponse?> GetById(string id)
        {
            VideoRequest videoRequest = _videoRequestRepository.GetById(id);
            var bytes = await _fileStorageRepository.DownloadFileAsync(videoRequest.imagesFileName,"");
            GetImagesResponse response = new()
            {
                Images = bytes.ToArray(),
                FileName = videoRequest.imagesFileName,
            };
            return response;
        }
    }
}
