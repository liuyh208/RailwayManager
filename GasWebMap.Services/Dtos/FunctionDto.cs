using System;
using System.Collections.Generic;
using GasWebMap.Services.Responses;
using ServiceStack.ServiceHost;

namespace GasWebMap.Services.Dtos
{
    [Route("/rolefuntion", "POST")]
    public class RoleFun
    {
        public RoleFun()
        {
            FunctionIDs=new List<Guid>();
        }

        public Guid RoleID { get; set; }

        public List<Guid> FunctionIDs { get; set; }
    }

    [Route("/function", "POST,PUT")]
    public class FunctionDto
    {
        public Guid ID { get; set; }
        public string Text { get; set; }

        public string Url { get; set; }
        public Guid GroupID { get; set; }
    }

    [Route("/function", "GET")]
    public class FunctionGet
    {
        public Guid GroupID { get; set; }
    }


    [Route("/updateLock","PUT")]
    public class FunctionUpdateLock
    {
        public List<Guid> FunctionIds { get; set; }
        public bool IsLock { get; set; }
    }

    [Route("/function2", "DELETE")]
    [Serializable]
    public class FunctionDelete : DeleteOperation<Guid>
    {
    }
}