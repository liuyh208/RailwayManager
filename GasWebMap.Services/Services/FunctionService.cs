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
    public class FunctionService : ServiceBase
    {
        private IList<MenuInfo> lst;

        public IList<MenuInfo> Get(FunctionGet functionGet)
        {
            IRepository<MenuInfo> rep = GetRepository<MenuInfo>();
            if (functionGet.GroupID == Guid.Empty ||
                functionGet.GroupID == Guid.Parse("f0e27d26-e143-4ce1-a681-bb64d5a8c6b4"))
            {
                lst = rep.GetEntities(t => t.Text != null).ToList();
            }
            else
            {
                lst = rep.GetEntities(t => t.GroupID == functionGet.GroupID).ToList();
            }
            return lst;
        }

        public MenuInfo Post(FunctionDto dto)
        {
            RemoveCache(CacheKeys.MenuKey);
            var f = new MenuInfo {Text = dto.Text,Url = dto.Url, Source = "local",GroupID=dto.GroupID,Id = Guid.NewGuid()};
            IRepository<MenuInfo> rep = GetRepository<MenuInfo>();
            rep.Add(f);
            return f;
        }

        public MenuInfo Put(FunctionDto dto)
        {
            RemoveCache(CacheKeys.MenuKey);
            IRepository<MenuInfo> rep = GetRepository<MenuInfo>();
            MenuInfo f = rep.GetEntityByID(dto.ID);
            if (f != null)
            {
                f.Id = dto.ID;
                f.Text = dto.Text;
                f.Url = dto.Url;
                rep.Update(f);
                return f;
            }
            return new MenuInfo();
        }

        public ResponseResult Delete(FunctionDelete functions)
        {
            RemoveCache(CacheKeys.MenuKey);
            IRepository<MenuInfo> rep = GetRepository<MenuInfo>();
            rep.DeleteByIDs(functions);
            return ResponseResult.SuccessRes;
        }

        public ResponseResult Post(RoleFun roleFun)
        {
            IRepository<RoleMenu> rep = GetRepository<RoleMenu>();
            rep.Delete(t => t.RoleID == roleFun.RoleID);
            IList<RoleMenu> list = new List<RoleMenu>();

            foreach (Guid item in roleFun.FunctionIDs)
            {
                list.Add(new RoleMenu {Id =Guid.NewGuid(), RoleID = roleFun.RoleID, MenuID = item});
            }
            rep.Add(list);

            return ResponseResult.SuccessRes;
        }

        public ResponseResult Put(FunctionUpdateLock funLock)
        {
            IRepository<MenuInfo> rep = GetRepository<MenuInfo>();
            foreach (var item in funLock.FunctionIds)
            {
                MenuInfo f = rep.GetEntityByID(item);
                if (f != null) { 
                    f.Id=item;
                    f.IsLock = funLock.IsLock;
                    rep.Update(f);
                }

            }
            return ResponseResult.SuccessRes;
        }
    }
}