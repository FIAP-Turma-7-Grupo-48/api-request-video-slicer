using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Api.Request.Video.Slicer.UseCase.Dtos;
using Api.Request.Video.Slicer.Domain;
using Api.Request.Video.Slicer.Domain.Entities.Dtos.VideoRequestResponse;
using Api.Request.Video.Slicer.Domain.Enum;


namespace Api.Request.Video.Slicer.UseCase.UseCase.Interfaces
{
    public interface IVideoRequestUseCase
    {
        Task<GetImagesResponse?> GetById(string id);
        Task<VideoRequest> CreateAsync(CreateVideoRequestRequest createCustomerRequest, string userId);
        Task<IEnumerable<VideoRequest>> ListAsync(IEnumerable<RequestStatus> orderStatus, string userId, int? page, int? limit, CancellationToken cancellationToken);
        Task UpdateStatusAsync(UpdateVideoRequestStatus updateVideoRequestStatus);
    }
}
