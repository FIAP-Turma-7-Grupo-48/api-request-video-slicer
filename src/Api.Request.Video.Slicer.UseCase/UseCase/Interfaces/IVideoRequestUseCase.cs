using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Api.Request.Video.Slicer.UseCase.Dtos;
using Api.Request.Video.Slicer.Domain;


namespace Api.Request.Video.Slicer.UseCase.UseCase.Interfaces
{
    public interface IVideoRequestUseCase
    {
        Task<VideoRequest?> GetById(string id, CancellationToken cancellationToken);
        Task<VideoRequest> CreateAsync(CreateVideoRequestRequest createCustomerRequest, CancellationToken cancellationToken);
    }
}
