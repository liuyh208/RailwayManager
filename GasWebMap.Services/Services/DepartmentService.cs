using System;
using System.Collections.Generic;
using System.Linq;
using GasWebMap.Core.Data;
using GasWebMap.Domain;
using GasWebMap.Services.Base;
using GasWebMap.Services.Dtos;
using GasWebMap.Services.Responses;
using ServiceStack.ServiceInterface;

namespace GasWebMap.Services
{
    [Authenticate]
    public class DepartmentService : ServiceBase
    {
        public Department Post(DepartmentPost dto)
        {
            var d = new Department {Name = dto.Name, Id = Guid.NewGuid(), ParentID = dto.ParentID};
            IRepository<Department> rep = GetRepository<Department>();
            rep.Add(d);
            return d;
        }

        public Department Put(DepartmentPost dto)
        {
            IRepository<Department> rep = GetRepository<Department>();
            var id = dto.ID.ToString("N");//.Replace("-", "");
            Department u = rep.GetEntityByID(id);
            if (u != null)
            {
                u.Name = dto.Name;
                rep.Update(u);
                return u;
            }
            return new Department();
        }

        public ResponseResult Delete(DepartmentDeleteOne del)
        {
            IRepository<Department> rep = GetRepository<Department>();
            if (!string.IsNullOrWhiteSpace(del.Id))
            {
                var id = del.Id.Replace("-", "");
                rep.DeleteByID(id);
            }
            return ResponseResult.SuccessRes;
        }

        public IList<TreeNode> Get(Department depart)
        {
            IList<TreeNode> lstNode = new List<TreeNode>();
            IRepository<Department> rep = GetRepository<Department>();
            IEnumerable<Department> lst = rep.GetEntities(t => t.Id != null);

            IEnumerable<Department> lstParent = lst.Where(t => t.ParentID == null);
            foreach (Department department in lstParent)
            {
                var node = new TreeNode();
                node.id = department.Id.ToString();
                node.text = department.Name;
                addSubNode(node, lst);
                lstNode.Add(node);
            }
            return lstNode;
        }

        private void addSubNode(TreeNode node, IEnumerable<Department> departments)
        {
            if (node == null)
            {
                return;
            }
            Guid id = Guid.Parse(node.id);
            IEnumerable<Department> lst = departments.Where(t => t.ParentID == id);
            foreach (Department department in lst)
            {
                var snode = new TreeNode
                {
                    id = department.Id.ToString(),
                    text = department.Name
                };
                addSubNode(snode, departments);
                node.children.Add(snode);
            }
        }
    }
}