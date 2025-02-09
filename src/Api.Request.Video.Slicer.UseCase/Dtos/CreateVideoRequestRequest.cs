﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Api.Request.Video.Slicer.Domain.Enum;

namespace Api.Request.Video.Slicer.UseCase.Dtos
{
    public class CreateVideoRequestRequest
    {
        public string Id = Guid.NewGuid().ToString();
        public string Extension { get; set; }
        public string FileName { get; set; }
        public VideoType FileType { get; set; }
        public Stream Stream { get; set; }
        public string Path { get; set; }
        public string ContentType { get; set; }
    }
}
