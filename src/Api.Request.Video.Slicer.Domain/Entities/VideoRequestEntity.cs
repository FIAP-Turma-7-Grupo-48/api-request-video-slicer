using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Api.Request.Video.Slicer.Domain.Enum;
using MongoDB.Bson;
using Api.Request.Video.Slicer.Domain.Entities.Dtos.VideoRequestResponse;


namespace Api.Request.Video.Slicer.Domain
{
    public class VideoRequest
    {
        public VideoRequest() {
            this.id = Guid.NewGuid().ToString();
        }
        public string id;
        public string fileName;
        public string extension;
        public VideoTypeEnum videoTypeEnum;
        public string videoUrl;
        public string imagesFileName;
        public string imagesUrl;
    }

}
