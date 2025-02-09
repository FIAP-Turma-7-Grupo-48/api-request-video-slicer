using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Request.Video.Slicer.Infrastucture.Clients.Domain
{
    public class SendVideoRequest
    {
        public string RequestId { get; init; }
        public StorageFile StorageFile { get; init; }
    }
}
