using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Api.Request.Video.Slicer.UseCase.Dtos;
using Api.Request.Video.Slicer.Domain;
using Api.Request.Video.Slicer.Domain.Entities.Dtos.VideoRequestResponse;


namespace Api.Request.Video.Slicer.UseCase.UseCase.Interfaces
{
    public interface IVideoRequestUseCase
    {
        Task<GetImagesResponse?> GetById(string id);
        Task<VideoRequest> CreateAsync(CreateVideoRequestRequest createCustomerRequest);
    }
}
