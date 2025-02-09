using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Api.Request.Video.Slicer.Domain;
using Api.Request.Video.Slicer.Infrastructure.Context;
using Api.Request.Video.Slicer.Infrastucture.Repositories.interfaces;
using MongoDB.Bson;
using MongoDB.Driver;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Api.Request.Video.Slicer.Infrastucture.Repositories
{
    public class VideoRequestRepository : IVideoRequestRepository
    {
        public IMongoCollection<VideoRequest> _collectionVideoRequest { get; set; }
        public VideoRequestRepository(VideoSlicerDbContext context)
        {
            this._collectionVideoRequest = context._database.GetCollection<VideoRequest>("VideoRequest");
        }
        public Task Create(VideoRequest videoRequestEntity)
        {
            this._collectionVideoRequest.InsertOne(videoRequestEntity);
            return Task.CompletedTask;
        }

        public VideoRequest GetById(string id)
        {
            var filter = Builders<VideoRequest>.Filter.Eq(x => x.id, id);
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
