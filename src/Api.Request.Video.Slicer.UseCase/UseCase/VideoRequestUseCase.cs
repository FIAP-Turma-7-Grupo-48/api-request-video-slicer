using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Api.Request.Video.Slicer.Domain;
using Api.Request.Video.Slicer.Infrastucture.Repositories;
using Api.Request.Video.Slicer.Infrastucture.Repositories.interfaces;
using Api.Request.Video.Slicer.UseCase.Dtos;
using Api.Request.Video.Slicer.UseCase.UseCase.Interfaces;

namespace Api.Request.Video.Slicer.UseCase.UseCase
{
    public class VideoRequestUseCase : IVideoRequestUseCase
    {
        private readonly IVideoRequestRepository _videoRequestRepository;
        public VideoRequestUseCase(IVideoRequestRepository videoRequestRepository)
        { 
            _videoRequestRepository = videoRequestRepository;
        }
        public Task<VideoRequest> CreateAsync(CreateVideoRequestRequest createVideoRequestRequest, CancellationToken cancellationToken)
        {
            VideoRequest videoRequest = new()
            {                
                extension = createVideoRequestRequest.Extension,
                fileName = createVideoRequestRequest.FileName,
                videoTypeEnum = createVideoRequestRequest.fileType,
            };

            _videoRequestRepository.Create(videoRequest);
            return Task.FromResult(videoRequest);
        }

        public Task<VideoRequest?> GetById(string id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
