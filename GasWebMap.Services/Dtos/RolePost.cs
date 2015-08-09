using System;
using ServiceStack.ServiceHost;

namespace GasWebMap.Services.Dtos
{
    [Route("/role", "POST,PUT")]
    public class RoleDto
    {
        public Guid ID { get; set; }

        public string Name { get; set; }

        public int PIndex { get; set; }
    }

    [Route("/role", "GET")]
    public class RoleGet
    {
        public Guid UserId { get; set; }
    }

    /// <summary>
    ///     获得角色树数据
    /// </summary>
    [Route("/roletree", "GET")]
    public class RoleTree
    {
        public Guid? UserId { get; set; }
    }

        [Route("/userrole", "POST")]
        public class UserRoleRequest
        {
            public Guid RoleID { get; set; }
            //public UserRoleRequest() {
            //    this.RoleIds = new List<Guid>();
            //}
            //public IList<Guid> RoleIds { get; set; }
            public Guid UserId { get; set; }
        }

    [Route("/role/{Id}", "DELETE")]
    public class RoleDeleteOne
    {
        public Guid Id { get; set; }
    }
}