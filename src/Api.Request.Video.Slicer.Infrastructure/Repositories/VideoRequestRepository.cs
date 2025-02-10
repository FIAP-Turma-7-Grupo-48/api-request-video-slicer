using Api.Request.Video.Slicer.Domain;
using Api.Request.Video.Slicer.Domain.Enum;
using Api.Request.Video.Slicer.Infrastructure.Context;
using Api.Request.Video.Slicer.Infrastucture.Helpers;
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
        public async Task CreateAsync(VideoRequest videoRequestEntity)
        {
            await _collectionVideoRequest.InsertOneAsync(videoRequestEntity);

        }

        public async Task UpdateAsync(VideoRequest videoRequestEntity)
        {
            var filters = Builders<VideoRequest>.Filter.Eq(x => x.Id, videoRequestEntity.Id);
            var options = new FindOneAndReplaceOptions<VideoRequest>()
            {
                ReturnDocument = ReturnDocument.After,
                IsUpsert = false
            };

            var replacedOrder = await _collectionVideoRequest.FindOneAndReplaceAsync(filters, videoRequestEntity, options);
        }


        public async Task<IEnumerable<VideoRequest>> ListAsync(IEnumerable<RequestStatus> orderStatus, string userId, int? page, int? limit, CancellationToken cancellationToken)
        {
            var take = limit ?? int.MaxValue;

            var skip = MathHelper.CalculatePaginateSkip(page, take);

            var findOptions = new FindOptions<VideoRequest>()
            {
                Skip = skip,
                Limit = take,
            };

            var filters = Builders<VideoRequest>
                .Filter.And(
                    Builders<VideoRequest>.Filter.Eq(x => x.UserId, userId), 
                    Builders<VideoRequest>.Filter.In(x => x.Status, orderStatus)
                );

            var orders = await _collectionVideoRequest
                .FindAsync(filters, findOptions, cancellationToken);

            return orders.ToEnumerable();
        }

        public async Task<VideoRequest> GetByIdAsync(string id)
        {
            var filter = Builders<VideoRequest>.Filter.Eq(x => x.Id, id);
            return await _collectionVideoRequest.Find(filter).FirstOrDefaultAsync();
        }

    }
}
