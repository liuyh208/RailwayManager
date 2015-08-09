using System;
using ServiceStack.ServiceHost;

namespace GasWebMap.Services.Dtos
{
    [Route("/department", "POST,PUT")]
    public class DepartmentPost
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public Guid? ParentID { get; set; }
    }

    [Route("/department/{Id}", "DELETE")]
    public class DepartmentDeleteOne
    {
        public string Id { get; set; }
    }
}