using System;
using System.Configuration;
using GasWebMap.Core;
using GasWebMap.Core.Data;
using GasWebMap.Domain;

namespace GasWebMap.Services.Host
{
    internal class InitData
    {
        public static void Init()
        {
            int v = ConfigurationManager.AppSettings["InitData"].ToInt();
            if (v > 0)
            {
                IRepository<MenuInfo> rep = AppEx.Container.GetRepository<MenuInfo>();
                IRepository<MenuInfoGroup> repg = AppEx.Container.GetRepository<MenuInfoGroup>();

                var mg = new MenuInfoGroup();
                mg.Id = Guid.NewGuid();
                mg.PIndex = 1;
                mg.Text = "地图定位";
                repg.Add(mg);

                var m = new MenuInfo();
                m.Id = Guid.NewGuid();
                m.GroupID = mg.Id;
                m.PIndex = 1;
                m.Text = "地名定位";
                rep.Add(m);

                m = new MenuInfo();
                m.Id = Guid.NewGuid();
                m.GroupID = mg.Id;
                m.PIndex = 2;
                m.Text = "设施定位";
                rep.Add(m);

                m = new MenuInfo();
                m.Id = Guid.NewGuid();
                m.GroupID = mg.Id;
                m.PIndex = 3;
                m.Text = "书签定位";
                rep.Add(m);

                mg = new MenuInfoGroup();
                mg.Id = Guid.NewGuid();
                mg.PIndex = 2;
                mg.Text = "综合分析";
                repg.Add(mg);

                m = new MenuInfo();
                m.Id = Guid.NewGuid();
                m.GroupID = mg.Id;
                m.PIndex = 1;
                m.Text = "事故分析";
                rep.Add(m);

                m = new MenuInfo();
                m.Id = Guid.NewGuid();
                m.GroupID = mg.Id;
                m.PIndex = 2;
                m.Text = "连通性分析";
                rep.Add(m);

                mg = new MenuInfoGroup();
                mg.Id = Guid.NewGuid();
                mg.PIndex = 3;
                mg.Text = "系统设置";
                repg.Add(mg);

                m = new MenuInfo();
                m.Id = Guid.NewGuid();
                m.GroupID = mg.Id;
                m.PIndex = 1;
                m.Text = "部门管理";
                rep.Add(m);

                m = new MenuInfo();
                m.Id = Guid.NewGuid();
                m.GroupID = mg.Id;
                m.PIndex = 2;
                m.Text = "用户管理";
                rep.Add(m);

                m = new MenuInfo();
                m.Id = Guid.NewGuid();
                m.GroupID = mg.Id;
                m.PIndex = 3;
                m.Text = "角色管理";
                rep.Add(m);

                m = new MenuInfo();
                m.Id = Guid.NewGuid();
                m.GroupID = mg.Id;
                m.PIndex = 4;
                m.Text = "菜单管理";
                rep.Add(m);
            }
        }
    }
}