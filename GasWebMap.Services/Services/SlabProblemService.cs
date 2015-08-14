using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web.Helpers;
using System.Web.Mvc;
using AutoMapper;
using GasWebMap.Core;
using GasWebMap.Core.Data;
using GasWebMap.Domain;
using GasWebMap.Services.Base;
using GasWebMap.Services.Dtos;
using GasWebMap.Services.Host;
using GasWebMap.Services.Responses;
using ServiceStack.Common.Web;
using ServiceStack.ServiceHost;

namespace GasWebMap.Services.Services
{
    public class SlabProblemService : ServiceBase
    {
        public PageData<SlabProblem> Get(SlabGet request)
        {
            var rep = AppEx.Container.GetRepository<SlabProblem>();
            CustomUserSession sn = this.SessionAs<CustomUserSession>();
            bool bl = request.order == "asc";

            Expression<Func<SlabProblem, bool>> filter = t => t.DepartmentID == sn.DepartmentID;
            if (!string.IsNullOrWhiteSpace(request.RailWay))
            {
                filter = filter.And(t => t.RailWay == request.RailWay);
            }

            if (!string.IsNullOrWhiteSpace(request.RunType))
            {
                filter = filter.And(t => t.RunType == request.RunType);
            }
            if (!string.IsNullOrWhiteSpace(request.HurtItem))
            {
                filter = filter.And(t => t.HurtItem == request.HurtItem);
            }
            if (!string.IsNullOrWhiteSpace(request.HurtLevel))
            {
                filter = filter.And(t => t.HurtLevel == request.HurtLevel);
            }
            if (!string.IsNullOrWhiteSpace(request.HurtPosition))
            {
                filter = filter.And(t => t.HurtPosition == request.HurtPosition);
            }

            if (!string.IsNullOrWhiteSpace(request.HurtStyle))
            {
                filter = filter.And(t => t.HurtStyle == request.HurtStyle);
            }

            if (request.Mileage!=0.0)
            {
                filter = filter.And(t => t.Mileage >= request.Mileage);
            }

            if (request.Mileage2 != 0.0)
            {
                filter = filter.And(t => t.Mileage < request.Mileage2);
            }

            if (request.CheckDate!=DateTime.MinValue)
            {
                filter = filter.And(t => t.CheckDate >= request.CheckDate);
            }
            if (request.CheckDate2 != DateTime.MinValue)
            {
                filter = filter.And(t => t.CheckDate < request.CheckDate2);
            }

            if (request.LogoutDate != DateTime.MinValue)
            {
                filter = filter.And(t => t.LogoutDate >= request.LogoutDate);
            }
            if (request.LogoutDate2 != DateTime.MinValue)
            {
                filter = filter.And(t => t.LogoutDate < request.LogoutDate2);
            }

            var result = rep.GetPagedEntities(filter, t => t.CheckDate, bl, request.Page, request.Rows);

            return new PageData<SlabProblem>(result.Result, result.Total);
        }

        /// <summary>
        /// add
        /// </summary>
        /// <param name="request">The request.</param>
        public ResponseResult Post(SlabProblemDto request)
        {
            
            try
            {
                var rep = AppEx.Container.GetRepository<SlabProblem>();
                request.Id = Guid.NewGuid();
                CustomUserSession sn = this.SessionAs<CustomUserSession>();
                request.DepartmentID = sn.DepartmentID;
                var item = Mapper.Map<SlabProblemDto, SlabProblem>(request);
                rep.Add(item);
                return ResponseResult.SuccessRes ;
            }
            catch (Exception ex)
            {
                return ResponseResult.FailureRes("保存失败");
            }
           
        }


        public ResponseResult Post(SlabProblemEdit request)
        {
            try
            {
                var rep = AppEx.Container.GetRepository<SlabProblem>();
                var item = Mapper.Map<SlabProblemEdit, SlabProblem>(request);
                CustomUserSession sn = this.SessionAs<CustomUserSession>();
                item.DepartmentID = sn.DepartmentID;
                rep.Update(item);
                return ResponseResult.SuccessRes;
            }
            catch (Exception ex)
            {
                return ResponseResult.FailureRes("保存失败");
            }
           
          
           
        }

        public ResponseResult Post(DeleteSlab request)
        {
            
            try
            {
                var rep = AppEx.Container.GetRepository<SlabProblem>();
                rep.DeleteByIDs(request);
                return ResponseResult.SuccessRes;
            }
            catch (Exception ex)
            {
                return ResponseResult.FailureRes("保存失败");
            }
        }

    }
}
