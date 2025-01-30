using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Api.Request.Video.Slicer.Domain.Enum;
using MongoDB.Bson;


namespace Api.Request.Video.Slicer.Domain
{
    public class VideoRequest
    {
        public ObjectId id;
        public string fileName;
        public string extension;
        public VideoTypeEnum videoTypeEnum;
        
    }

}
