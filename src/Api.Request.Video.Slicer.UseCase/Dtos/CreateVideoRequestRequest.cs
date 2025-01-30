using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Api.Request.Video.Slicer.Domain.Enum;

namespace Api.Request.Video.Slicer.UseCase.Dtos
{
    public class CreateVideoRequestRequest
    {
        public string Id { get; set; }
        public string Extension { get; set; }
        public string FileName { get; set; }
        public VideoTypeEnum fileType { get; set; }
    }
}
