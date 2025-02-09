using Api.Request.Video.Slicer.Domain;

namespace Api.Request.Video.Slicer.Infrastucture.Repositories.interfaces
{
    public interface IVideoRequestRepository
    {
        public Task Create(VideoRequest videoRequestEntity);
        public Task Update(VideoRequest videoRequestEntity);
        Task UpdateAsync(VideoRequest videoRequestEntity);
        public List<VideoRequest> GetPendingRequests();
        public VideoRequest GetById(string id);
    }
}
