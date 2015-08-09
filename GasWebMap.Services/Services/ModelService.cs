using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GasWebMap.Domain;
using GasWebMap.Services.Base;
using GasWebMap.Services.Dtos;
using GasWebMap.Services.Responses;

namespace GasWebMap.Services.Services
{
    public class ModelService : ServiceBase
    {
        public MenuInfoGroup Post(ModelDto dto)
        {
            var m = new MenuInfoGroup { PIndex = dto.PIndex,Text=dto.Text, Id = Guid.NewGuid() };
            var rep = GetRepository<MenuInfoGroup>();
            rep.Add(m);
            return m;
        }

        public MenuInfoGroup Put(ModelDto dto)
        {
            var rep=GetRepository<MenuInfoGroup>();
            MenuInfoGroup m = new MenuInfoGroup();
            m = rep.GetEntityByID(dto.ID);
            //var m = lst.FirstOrDefault(t => t.Id == Guid.Parse(dto.ID));
            if (m != null)
            {
                m.Id = dto.ID;
                m.PIndex = dto.PIndex;
                m.Text = dto.Text;
                rep.Update(m);
                return m;

            }
            return new MenuInfoGroup();
        }

        public ResponseResult Delete(ModelDelete del)
        {
            foreach (var item in del)
            {
                var rep = GetRepository<MenuInfoGroup>();
                MenuInfoGroup m = new MenuInfoGroup();
                m = rep.GetEntityByID(item.Value);
                var id = m.Id;
                //var id = lst.FirstOrDefault(t => t.Id == item.Value);
                if (id != null)
                {
                    rep.DeleteByID(id);
                }
            }
            return ResponseResult.SuccessRes;
        }
    }
}
