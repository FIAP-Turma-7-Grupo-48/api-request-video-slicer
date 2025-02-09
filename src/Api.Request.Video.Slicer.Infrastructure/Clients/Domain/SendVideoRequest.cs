using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Request.Video.Slicer.Infrastucture.Clients.Domain
{
    public class SendVideoRequest
    {
        public string fileName { get; set; }
        public string videoUrl { get; set; }
        public string id { get; set; }
    }
}
