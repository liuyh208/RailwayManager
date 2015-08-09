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
    public class GroupService : ServiceBase
    {
        public IList<TreeNode> Get(MenuInfoGroup menu)
        {
            IList<TreeNode> lstNode = new List<TreeNode>();
            IRepository<MenuInfoGroup> rep = GetRepository<MenuInfoGroup>();
            IEnumerable<MenuInfoGroup> lst = rep.GetEntities(t => t.Id != null);

            IEnumerable<MenuInfoGroup> lstParent = lst.Where(t => t.ParentID == null);
            foreach (MenuInfoGroup menugroup in lstParent)
            {
                var node = new TreeNode();
                node.id = menugroup.Id.ToString();
                node.text = menugroup.Text;
                addSubNode(node, lst);
                lstNode.Add(node);
            }
            return lstNode;
        }

        private void addSubNode(TreeNode node, IEnumerable<MenuInfoGroup> groups)
        {
            if (node == null)
            {
                return;
            }
            Guid id = Guid.Parse(node.id);
            IEnumerable<MenuInfoGroup> lst = groups.Where(t => t.ParentID == id);
            foreach (MenuInfoGroup menugroup in lst)
            {
                var snode = new TreeNode
                {
                    id = menugroup.Id.ToString(),
                    text = menugroup.Text,
                };
                addSubNode(snode, groups);
                node.children.Add(snode);
            }
        }

        public MenuInfoGroup Post(GroupDto dto)
        {
            var m = new MenuInfoGroup
            {
                PIndex = dto.PIndex,
                Text = dto.Text,
                Id = Guid.NewGuid(),
                ParentID = dto.ParentID
            };
            IRepository<MenuInfoGroup> rep = GetRepository<MenuInfoGroup>();
            rep.Add(m);
            return m;
        }

        public MenuInfoGroup Put(GroupDto dto)
        {
            IRepository<MenuInfoGroup> rep = GetRepository<MenuInfoGroup>();
            MenuInfoGroup m = rep.GetEntityByID(dto.ID);
            if (m != null)
            {
                m.Id = dto.ID;
                m.Text = dto.Text;
                rep.Update(m);
                return m;
            }
            return new MenuInfoGroup();
        }

        public ResponseResult Delete(GroupDeleteOne item)
        {
            var rep = GetRepository<MenuInfo>();
            rep.Delete(t => t.GroupID == item.Id.ToGuid());
            string id = item.Id;
            if (id != null)
            {
                rep.DeleteByID(id);
            }
            return ResponseResult.SuccessRes;
        }
    }
}