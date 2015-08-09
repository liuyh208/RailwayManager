using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using GasWebMap.Core;
using GasWebMap.Core.Data;
using GasWebMap.Domain;
using GasWebMap.Services.Base;
using GasWebMap.Services.Dtos;
using GasWebMap.Services.Responses;
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

            Expression<Func<SlabProblem, bool>> filter = t => t.Id!=null;
            if (!string.IsNullOrWhiteSpace(request.RailWay))
            {
                filter = filter.And(t => t.RailWay == request.RailWay);
            }

            if (!string.IsNullOrWhiteSpace(request.RunType))
            {
                filter = filter.And(t => t.RunType == request.RunType);
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
                var item = Mapper.Map<SlabProblemDto, SlabProblem>(request);
                rep.Add(item);
                return ResponseResult.SuccessRes;
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
