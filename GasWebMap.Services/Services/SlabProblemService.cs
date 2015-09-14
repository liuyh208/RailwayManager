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
            var result = GetData(request);
            return new PageData<SlabProblem>(result.Result, result.Total);
        }

        private PageResult<SlabProblem> GetData(SlabGet request)
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

            if (request.Mileage != 0.0)
            {
                filter = filter.And(t => t.Mileage >= request.Mileage);
            }

            if (request.Mileage2 != 0.0)
            {
                filter = filter.And(t => t.Mileage < request.Mileage2);
            }

            if (request.CheckDate != DateTime.MinValue)
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
            //var rrr=rep.GetEntities(filter,t=>t.CheckDate)
            var result = rep.GetPagedEntities(filter, t => t.CheckDate, bl, request.Page, request.Rows);

            return result;
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

        public object Get(SlabExport slab)
        {
            slab.Page = 0;
            slab.Rows = 100000;
            var result = GetData(slab);
            var rep = GetRepository<Department>();
            CustomUserSession sn = this.SessionAs<CustomUserSession>();
          

            var sbHtml = new StringBuilder();
            sbHtml.Append("<table border='1' cellspacing='0' cellpadding='0'>");
            sbHtml.Append("<tr>");
            sbHtml.AppendFormat("<td align=center colspan=21 height='50' style='font-size:16.0pt;font-weight:700;'>{0}</td>", "轨道板问题库");
            sbHtml.Append("</tr>");
            sbHtml.Append("<tr>");
            sbHtml.AppendFormat("<td align=center colspan=2 height='50' style='font-size:14.0pt;'>{0}</td>", "车间：");
            sbHtml.AppendFormat("<td  colspan=2 height='50' style='font-size:14.0pt;'>{0}</td>", sn.Department);
            sbHtml.Append("</tr>");
            sbHtml.Append("<tr>");
            var lstTitle = new List<string> { "序号", "线别", "行别", "对应里程", "起点板号", "承轨台", "设备情况", "伤损项目", "伤损位置", "伤损形式", "长", "宽", "高", "伤损等级", "检查日期", "检查人", "影像资料", "采取措施", "销号日期", "销号人", "备注" };
            foreach (var item in lstTitle)
            {
                sbHtml.AppendFormat("<td style='font-size: 14px;text-align:center;background-color: #DCE0E2; font-weight:bold;' height='25'>{0}</td>", item);
            }
            sbHtml.Append("</tr>");

            int i = 1;

            foreach (var info in result.Result)
            {
                sbHtml.Append("<tr>");
                sbHtml.AppendFormat("<td style='font-size: 12px;height:20px;'>{0}</td>", i);
                sbHtml.AppendFormat("<td style='font-size: 12px;height:20px;'>{0}</td>", info.RailWay);
                sbHtml.AppendFormat("<td style='font-size: 12px;height:20px;'>{0}</td>", info.RunType);
                sbHtml.AppendFormat("<td style='font-size: 12px;height:20px;'>{0}</td>", info.Mileage);
                sbHtml.AppendFormat("<td style='font-size: 12px;height:20px;'>{0}</td>", info.StartBoardID);
                sbHtml.AppendFormat("<td style='font-size: 12px;height:20px;'>{0}</td>", info.SupportRailID);
                sbHtml.AppendFormat("<td style='font-size: 12px;height:20px;'>{0}</td>", info.DeviceDesc);
                sbHtml.AppendFormat("<td style='font-size: 12px;height:20px;'>{0}</td>", info.HurtItem);
                sbHtml.AppendFormat("<td style='font-size: 12px;height:20px;'>{0}</td>", info.HurtPosition);
                sbHtml.AppendFormat("<td style='font-size: 12px;height:20px;'>{0}</td>", info.HurtStyle);
                sbHtml.AppendFormat("<td style='font-size: 12px;height:20px;'>{0}</td>", info.Length);
                sbHtml.AppendFormat("<td style='font-size: 12px;height:20px;'>{0}</td>", info.Width);
                sbHtml.AppendFormat("<td style='font-size: 12px;height:20px;'>{0}</td>", info.Height);
                sbHtml.AppendFormat("<td style='font-size: 12px;height:20px;'>{0}</td>", info.HurtLevel);

                sbHtml.AppendFormat("<td style='font-size: 12px;height:20px;'>{0}</td>", info.CheckDate != DateTime.MinValue ? info.CheckDate.ToString("yyyy-MM-dd") : "");
                sbHtml.AppendFormat("<td style='font-size: 12px;height:20px;'>{0}</td>", info.Checker);
                sbHtml.AppendFormat("<td style='font-size: 12px;height:20px;'>{0}</td>", info.Movie);
                sbHtml.AppendFormat("<td style='font-size: 12px;height:20px;'>{0}</td>", info.TakeMeasures);
                sbHtml.AppendFormat("<td style='font-size: 12px;height:20px;'>{0}</td>", info.LogoutDate != DateTime.MinValue ? info.LogoutDate.ToString("yyyy-MM-dd") : "");
                sbHtml.AppendFormat("<td style='font-size: 12px;height:20px;'>{0}</td>", info.LogoutMan);
                sbHtml.AppendFormat("<td style='font-size: 12px;height:20px;'>{0}</td>", info.Remark);
                sbHtml.Append("</tr>");
                i++;
            }


            sbHtml.Append("</table>");

            //第一种:使用FileContentResult
            byte[] fileContents = Encoding.Default.GetBytes(sbHtml.ToString());
            var fileStream = new MemoryStream(fileContents);
            return new ExcelResult(fileStream);
        }
    }
}
