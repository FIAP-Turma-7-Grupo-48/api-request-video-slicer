using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Request.Video.Slicer.Infrastucture.Clients.Domain
{
    public struct StorageFile
    {
        public StorageFile()
        {

        }
        public string Key { get; set; } = string.Empty;
        public string FileName { get; set; } = string.Empty;
        public string ContentType { get; set; } = string.Empty;
    }
}
