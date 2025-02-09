using Api.Request.Video.Slicer.Domain;
using Api.Request.Video.Slicer.Infrastructure.Context;
using Api.Request.Video.Slicer.Infrastucture.Repositories.interfaces;
using MongoDB.Driver;

namespace Api.Request.Video.Slicer.Infrastucture.Repositories
{
    public class VideoRequestRepository : IVideoRequestRepository
    {
        public IMongoCollection<VideoRequest> _collectionVideoRequest { get; set; }
        public VideoRequestRepository(VideoSlicerDbContext context)
        {
            this._collectionVideoRequest = context._database.GetCollection<VideoRequest>("VideoRequest");
        }
        public async Task Create(VideoRequest videoRequestEntity)
        {
            await _collectionVideoRequest.InsertOneAsync(videoRequestEntity);

        }

        public async Task UpdateAsync(VideoRequest videoRequestEntity)
        {
            var filter = Builders<VideoRequest>.Filter.Eq(x => x.Id, videoRequestEntity.Id);
            await _collectionVideoRequest.ReplaceOneAsync(filter, videoRequestEntity);

        }

        public VideoRequest GetById(string id)
        {
            var filter = Builders<VideoRequest>.Filter.Eq(x => x.Id, id);
            return this._collectionVideoRequest.Find(filter).FirstOrDefault();
        }

        public List<VideoRequest> GetPendingRequests()
        {
            throw new NotImplementedException();
        }

        public Task Update(VideoRequest videoRequestEntity)
        {
            throw new NotImplementedException();
        }
    }
}
