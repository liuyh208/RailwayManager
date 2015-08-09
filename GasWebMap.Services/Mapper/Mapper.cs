using System;
using AutoMapper;
using GasWebMap.Domain;
using GasWebMap.Services.Dtos;

namespace GasWebMap.Services
{
    public class MapperHelper
    {
        public static void InitMapper()
        {
            Mapper.CreateMap<MenuInfo, MenuDto>();
            Mapper.CreateMap<DeviceEdit, DeviceInfo>();
            Mapper.CreateMap<DeviceAdd, DeviceInfo>();
           var map=  Mapper.CreateMap<DeviceInfo, DeviceDto>();
           
            map.ForMember(d => d.Valid, opt => opt.MapFrom(t =>GetValid(t.ValidDate)));
            map.ForMember(d => d.BuyDate,
                opt => opt.MapFrom(t => t.BuyDate.HasValue ? t.BuyDate.Value.ToString("yyyy-MM-dd") : ""));
            map.ForMember(d => d.ValidDate,
                opt => opt.MapFrom(t => t.ValidDate.HasValue ? t.ValidDate.Value.ToString("yyyy-MM-dd") : ""));

            map.ForMember(d => d.IdentifyDate,
                opt => opt.MapFrom(t => t.IdentifyDate.HasValue ? t.IdentifyDate.Value.ToString("yyyy-MM-dd") : ""));
            //map.ForMember(d => d.ShippingStatusName, opt => opt.MapFrom(t => GetSaleOrderStatusName(t.ShippingStatus)));

            Mapper.CreateMap<SlabProblemDto, SlabProblem>();

            Mapper.CreateMap<SlabProblemEdit, SlabProblem>();
            

        }

        private static int GetValid(DateTime? dt)
        {
            if (dt.HasValue)
            {
                return (int) ((dt.Value - DateTime.Now.Date).TotalDays);
                if (DateTime.Now.Date >= dt.Value) //超期
                {
                    return 10;
                }
                else if (DateTime.Now.Date.AddMonths(1) >= dt) //一个月内
                {
                    return 5;
                }
                return 0;
            }
            return int.MaxValue;
        }
    }
}