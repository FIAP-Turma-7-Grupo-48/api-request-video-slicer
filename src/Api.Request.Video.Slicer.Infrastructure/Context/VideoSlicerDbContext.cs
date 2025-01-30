using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Api.Request.Video.Slicer.Infrastucture.Context;
using MongoDB.Driver;

namespace Api.Request.Video.Slicer.Infrastructure.Context
{
    public class VideoSlicerDbContext
    {
        public readonly IMongoDatabase _database;

        public VideoSlicerDbContext(MongoDbConfig mongoDbConfig)
        {            
            var client = new MongoClient(mongoDbConfig.ConnectionString);
            _database = client.GetDatabase(mongoDbConfig.DatabaseName);
        }
    }
}
