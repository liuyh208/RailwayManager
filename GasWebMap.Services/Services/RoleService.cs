using System;
using System.Collections.Generic;
using System.Linq;
using GasWebMap.Core.Data;
using GasWebMap.Domain;
using GasWebMap.Services.Base;
using GasWebMap.Services.Dtos;
using GasWebMap.Services.Responses;
using ServiceStack.ServiceInterface;

namespace GasWebMap.Services.Services
{
    [Authenticate]
    public class RoleService : ServiceBase
    {
        public IList<Role> Get(RoleGet roleGet)
        {
            //todo  add auth
            IRepository<Role> rep = GetRepository<Role>();

            IEnumerable<Role> lst = rep.GetEntities(t => t.Id != null);
            return new List<Role>(lst);
        }

        public IList<TreeNode> Get(RoleTree roleGet)
        {
            IRepository<Role> rep = GetRepository<Role>();

            IOrderedEnumerable<Role> lst = rep.GetEntities(t => t.Id != null).OrderBy(t => t.PIndex);
            IList<TreeNode> lstNodes = new List<TreeNode>();
            IRepository<UserRole> rep2 = GetRepository<UserRole>();
            IEnumerable<UserRole> lstUserRoles = new List<UserRole>();
            if (roleGet.UserId.HasValue)
            {
                lstUserRoles = rep2.GetEntities(t => t.UserID == roleGet.UserId.Value);
            }
            foreach (Role role in lst)
            {
                var node = new TreeNode
                {
                    text = role.Name,
                    id = role.Id.ToString()
                };
                UserRole ll = lstUserRoles.FirstOrDefault(t => t.RoleID == role.Id);
                if (ll != null)
                {
                    node.@checked = true;
                }
                lstNodes.Add(node);
            }
            return lstNodes;
        }

        public Role Post(RoleDto dto)
        {
            var f = new Role {Name = dto.Name, PIndex = dto.PIndex, Id = Guid.NewGuid()};
            IRepository<Role> rep = GetRepository<Role>();
            rep.Add(f);
            return f;
        }

        public Role Put(RoleDto dto)
        {
            IRepository<Role> rep = GetRepository<Role>();
            Role r = rep.GetEntityByID(dto.ID);
            if (r != null)
            {
                r.Id = dto.ID;
                r.Name = dto.Name;
                r.PIndex = dto.PIndex;
                rep.Update(r);
                return r;
            }
            return new Role();
        }

        public ResponseResult Delete(RoleDeleteOne item)
        {
            IRepository<Role> rep = GetRepository<Role>();
            IRepository<RoleMenu> roleMenu = GetRepository<RoleMenu>();
            Guid id = item.Id;
            if (id != null)
            {
                rep.DeleteByID(id);
                roleMenu.Delete(t => t.RoleID == id);
            }

            return ResponseResult.SuccessRes;
        }

        public bool Post(UserRoleRequest request) {
            var rep = GetRepository<UserRole>();
            rep.Delete(t => t.UserID == request.UserId);
            var r = new UserRole { UserID = request.UserId, RoleID = request.RoleID, Id = Guid.NewGuid() };
            rep.Add(r);
            //var lst = new List<UserRole>();
            //foreach (var roleId in request.RoleIds)
            //{
            //    UserRole u = new UserRole();
            //    u.Id = Guid.NewGuid();
            //    u.RoleID = roleId;
            //    u.UserID = request.UserId;
            //    lst.Add(u);
            //}
            //rep.Add(lst);
            return true;
        }
    }
}