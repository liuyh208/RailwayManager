using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GasWebMap.Services.Responses;
using ServiceStack.ServiceHost;

namespace GasWebMap.Services.Dtos
{
    [Route("/model", "POST,PUT")]
    public class ModelDto
    {
        public Guid ID { get; set; }

        public int PIndex { get; set; }
        public string Text { get; set; }

    }

    [Route("/model", "DELETE")]
    public class ModelDelete : DeleteOperation
    {
    }
}
