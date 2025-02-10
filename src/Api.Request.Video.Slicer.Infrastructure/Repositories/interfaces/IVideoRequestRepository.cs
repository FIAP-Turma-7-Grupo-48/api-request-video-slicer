using Api.Request.Video.Slicer.Domain;
using Api.Request.Video.Slicer.Domain.Enum;

namespace Api.Request.Video.Slicer.Infrastucture.Repositories.interfaces
{
    public interface IVideoRequestRepository
    {
        public Task CreateAsync(VideoRequest videoRequestEntity);
        Task UpdateAsync(VideoRequest videoRequestEntity);
        Task<IEnumerable<VideoRequest>> ListAsync(IEnumerable<RequestStatus> orderStatus, int? page, int? limit, CancellationToken cancellationToken);
        public Task<VideoRequest> GetByIdAsync(string id);
    }
}
