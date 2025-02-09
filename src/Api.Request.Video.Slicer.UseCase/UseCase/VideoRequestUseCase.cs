using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Api.Request.Video.Slicer.Domain;
using Api.Request.Video.Slicer.Domain.Entities.Dtos.VideoRequestResponse;
using Api.Request.Video.Slicer.Domain.Enum;
using Api.Request.Video.Slicer.Domain.ValueObjects;
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
            VideoRequest videoRequest = new()
            {
                Extension = createVideoRequestRequest.Video.GetExtension(),
                FileName = createVideoRequestRequest.Video.FileName,
            };

            videoRequest.Video = new StorageFile
            {
                ContentType = createVideoRequestRequest.Video.ContentType,
                Key = videoRequest.Id,
                FileName = createVideoRequestRequest.Video.FileName,
            };

            await _fileStorageRepository.UploadFileAsync(createVideoRequestRequest.Video.Data, $"{videoRequest.Id}");

            await _videoRequestRepository.Create(videoRequest);

            await _videoSlicerClient.SendAsync(videoRequest);

            return Task.FromResult(videoRequest).Result;
        }

        public async Task UpdateStatusAsync(UpdateVideoRequestStatus updateVideoRequestStatus)
        {
            VideoRequest videoRequest = _videoRequestRepository.GetById(updateVideoRequestStatus.RequestId);

            videoRequest.Status = updateVideoRequestStatus.Status;
            if(updateVideoRequestStatus.Status == RequestStatus.Processed)
            {
                videoRequest.ZippedImg = updateVideoRequestStatus.File;
            }

            await _videoRequestRepository.UpdateAsync(videoRequest);

        }

        public async Task<GetImagesResponse?> GetById(string id)
        {

            
            VideoRequest videoRequest = _videoRequestRepository.GetById(id);

            if (videoRequest.Status != Domain.Enum.RequestStatus.Processed)
            {
                //Todo: dar error

            }
                var bytes = await _fileStorageRepository.DownloadFileAsync(videoRequest.ZippedImg!.Value.Key, "");
            GetImagesResponse response = new()
            {
                Images = bytes.ToArray(),
                FileName = videoRequest.ZippedImg.Value.Key,
            };
            return response;
        }
    }
}
