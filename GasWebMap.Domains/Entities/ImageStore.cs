using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GasWebMap.Core.Data;
using ServiceStack.ServiceHost;

namespace GasWebMap.Domain
{
    public class ImageStore : EntityBase
    {
        public Guid DeviceID { get; set; }

        public string Description { get; set; }

        public DateTime  DateTime { get; set; }

        public string FileName { get; set; }
    }
}
