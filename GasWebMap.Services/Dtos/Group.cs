using System;
using ServiceStack.ServiceHost;

namespace GasWebMap.Services.Dtos
{
    [Route("/group", "POST,PUT")]
    public class GroupDto
    {
        public Guid ID { get; set; }

        public int PIndex { get; set; }
        public string Text { get; set; }

        public Guid? ParentID { get; set; }
    }

    [Route("/group/{Id}", "DELETE")]
    public class GroupDeleteOne
    {
        public string Id { get; set; }
    }
}