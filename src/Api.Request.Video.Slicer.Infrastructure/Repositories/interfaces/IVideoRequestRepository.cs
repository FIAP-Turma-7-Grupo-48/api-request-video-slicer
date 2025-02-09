using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Api.Request.Video.Slicer.Domain;
using MongoDB.Bson;
using RabbitMQ.Client;

namespace Api.Request.Video.Slicer.Infrastucture.Repositories.interfaces
{
    public interface IVideoRequestRepository
    {
        public Task Create(VideoRequest videoRequestEntity);
        public Task Update(VideoRequest videoRequestEntity);        
        public List<VideoRequest> GetPendingRequests();
        public VideoRequest GetById(string id);
    }
}
