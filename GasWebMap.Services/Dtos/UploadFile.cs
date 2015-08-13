using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServiceStack.ServiceHost;

namespace GasWebMap.Services.Dtos
{
    [Route("/upload/{FileName}", "POST")]
    public class UploadPackage : IRequiresRequestStream
    {
        public System.IO.Stream RequestStream { get; set; }

        public string FileName { get; set; }
    }
}
