using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using GasWebMap.Core;
using GasWebMap.Core.Data;
using GasWebMap.Domain;
using GasWebMap.Services.Base;
using GasWebMap.Services.Dtos;
using ServiceStack.ServiceInterface;

namespace GasWebMap.Services
{
    [Authenticate]
    public class MenuService : ServiceBase
    {

       

        public IList<MenuGroupDto> GetGroupDtos(bool bl)
        {
            var lst = new List<MenuGroupDto>();
            //var mgroup = new MenuGroupDto();
            //mgroup.Text = "台账管理";
            //mgroup.Items.Add(new MenuDto() { Text = "台账管理", Url = "/device" });
            //lst.Add(mgroup);
            if (bl)
            {
               var   mgroup = new MenuGroupDto();
                mgroup.Text = "系统管理";
                mgroup.Items.Add(new MenuDto() { Text = "用户管理", Url = "/User/Manager" });
                mgroup.Items.Add(new MenuDto() { Text = "轨道板问题库", Url = "/Slab/index" });
                // mgroup.Items.Add(new MenuDto() { Text = "权限管理", Url = "/UserRole" });
                lst.Add(mgroup);
            }
          
           


            return lst;
        }

      

        public IList<MenuGroupDto> Get(MenuGet menu)
        {
            IList<MenuInfo> lst = GetCacheList(CacheKeys.MenuKey, () =>
            {
                IRepository<MenuInfo> rep = AppEx.Container.GetRepository<MenuInfo>();
                return rep.GetEntities().ToList();
            });

            string source = string.IsNullOrWhiteSpace(menu.From) ? "local" : menu.From.ToLower();
            IEnumerable<MenuInfo> lst2 = lst.Where(t => t.Source.Contains(source));

            IList<MenuInfoGroup> lstGroup = GetCacheList(CacheKeys.MenuGroupKey, () =>
            {
                IRepository<MenuInfoGroup> rep2 = AppEx.Container.GetRepository<MenuInfoGroup>();
                return rep2.GetEntities().ToList();
            });

            IList<MenuGroupDto> lstData = new List<MenuGroupDto>();
            IOrderedEnumerable<MenuInfoGroup> lstGroup2 = lstGroup.OrderBy(t => t.PIndex);
            foreach (MenuInfoGroup g in lstGroup2)
            {
                IEnumerable<MenuInfo> ll = lst2.Where(t => t.GroupID == g.Id);
                if (ll.Count() > 0)
                {
                    var mGroup = new MenuGroupDto();
                    mGroup.Text = g.Text;

                    foreach (MenuInfo item in ll)
                    {
                        mGroup.Items.Add(Mapper.Map<MenuInfo, MenuDto>(item));
                    }
                    lstData.Add(mGroup);
                }
            }

            return lstData;
        }

        public IList<TreeNode> Get(MenuTree role)
        {
            var rootNode = new TreeNode();
            rootNode.text = "所有功能";

            IRepository<MenuInfoGroup> menuInfoGroup = GetRepository<MenuInfoGroup>();
            IOrderedEnumerable<MenuInfoGroup> lst = menuInfoGroup.GetEntities().OrderBy(t => t.PIndex);
            
            //角色功能表
            
            IEnumerable<RoleMenu> lstRoleMenus = new List<RoleMenu>();
            //功能表
            IRepository<MenuInfo> menuRep = GetRepository<MenuInfo>();
            var lstMenu = menuRep.GetEntities(t => t.IsLock == false);
            
            if (!string.IsNullOrWhiteSpace(role.RoleId))
            {
                var rguid = Guid.Parse(role.RoleId);
                IRepository<RoleMenu> roleMenu = GetRepository<RoleMenu>();
                lstRoleMenus = roleMenu.GetEntities(t => t.RoleID == rguid);
            }

            IList<TreeNode> lstNodes=new List<TreeNode>();
            var lstParentMenuGroup=  menuInfoGroup.GetEntities(t => t.ParentID == null);
            foreach (var @group in lstParentMenuGroup)
            {
               var subNode = new TreeNode();
                subNode.id = @group.Id.ToString();
                subNode.text = @group.Text;
                lstNodes.Add(subNode);
                AddSubNode(subNode, lst.ToList(), lstRoleMenus,lstMenu);
            }
           
            return lstNodes;
        }

        private void AddSubNode(TreeNode node, IList<MenuInfoGroup> lstGroups, IEnumerable<RoleMenu> lstRoleMenus, IEnumerable<MenuInfo> listMenuInfos)
        {
            var lst = lstGroups.Where(t => t.ParentID ==Guid.Parse(node.id)).OrderBy(t=>t.PIndex);
            if (lst.Count()==0)
            {

                var lstMenu = listMenuInfos.Where(t => t.IsLock == false && t.GroupID == Guid.Parse(node.id));
                AddFunctionNode(node, lstMenu, lstRoleMenus);
                return;
                ;
            }

            foreach (var menuInfoGroup in lst)
            {
                var subNode = new TreeNode();
                subNode.id = menuInfoGroup.Id.ToString();
                subNode.text = menuInfoGroup.Text;
                
                AddSubNode(subNode, lstGroups, lstRoleMenus, listMenuInfos);
                if (subNode.children.Count>0)
                {
                node.children.Add(subNode);
                }
                
            }
        }

        private void AddFunctionNode(TreeNode node, IEnumerable<MenuInfo> lstMenuInfos,IEnumerable<RoleMenu> lstRoleMenus)
        {
            foreach (var menuInfo in lstMenuInfos)
            {
                var subNode = new TreeNode();
                subNode.id = menuInfo.Id.ToString();
                subNode.text = menuInfo.Text;

                RoleMenu ll = lstRoleMenus.FirstOrDefault(t => t.MenuID ==menuInfo.Id);
                if (ll != null)
                    subNode.@checked = true;

                node.children.Add(subNode);
            }
        }
    }
}