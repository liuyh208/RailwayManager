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
            var result = rep.GetPagedEntities(filter, t => t.CheckDate, bl, request.Page, request.Rows);

            return new PageData<SlabProblem>(result.Result, result.Total);
        }

        public object Post(UploadPackage request)
        {
            // hack - get the properties from the request
            if (string.IsNullOrEmpty(request.FileName))
            {
                var segments = base.Request.PathInfo.Split(new[] { '/' },
                    StringSplitOptions.RemoveEmptyEntries);
                request.FileName = segments[1];
            }
            
            string resultFile = Path.Combine(@"C:\Temp", request.FileName);
            if (File.Exists(resultFile))
            {
                File.Delete(resultFile);
            }
            using (FileStream file = File.Create(resultFile))
            {
                Copy(request.RequestStream,file);
            }

            return new HttpResult(System.Net.HttpStatusCode.OK);
        }

        private void Copy( Stream instance, Stream target)
        {
            int bytesRead = 0;
            int bufSize = copyBuf.Length;

            while ((bytesRead = instance.Read(copyBuf, 0, bufSize)) > 0)
            {
                target.Write(copyBuf, 0, bytesRead);
            }
        }
        private  readonly byte[] copyBuf = new byte[0x1000];

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



        public object Get(DeviceExport device)
        {
            if (device == null)
            {
                device = new DeviceExport();
            }
            device.Valid();
            IRepository<DeviceInfo> rep = AppEx.Container.GetRepository<DeviceInfo>();

            CustomUserSession sn = this.SessionAs<CustomUserSession>();

            IEnumerable<DeviceInfo> result = null;

            if (sn.IsAdmin)
            {
                result = rep.GetEntities(t => t.ValidDate >= device.StartDate && t.ValidDate < device.EndDate);
            }
            else
            {
                result = rep.GetEntities(t => t.DepartmentID == sn.DepartmentID && t.ValidDate >= device.StartDate && t.ValidDate < device.EndDate);

            }

            if (!string.IsNullOrWhiteSpace(device.Name))
            {
                result = result.Where(t => t.Name != null && t.Name.Contains(device.Name));
            }

            if (!string.IsNullOrWhiteSpace(device.WorkShop))
            {

                result = result.Where(t => t.Workshop != null && t.Workshop.Contains(device.WorkShop));
            }
            result = result.OrderBy(t => t.ValidDate);

            var sbHtml = new StringBuilder();
            sbHtml.Append("<table border='1' cellspacing='0' cellpadding='0'>");
            sbHtml.Append("<tr>");
            var lstTitle = new List<string> { "编号", "车间", "仪器名称", "保管单位", "检定形式", "仪器类别", "出厂编号", "型号精度", "生产厂家", "溯源周期", "购置时间", "检定时间", "有效期", "检定单位", "状态", "检定证书编号", "备注" };
            foreach (var item in lstTitle)
            {
                sbHtml.AppendFormat("<td style='font-size: 14px;text-align:center;background-color: #DCE0E2; font-weight:bold;' height='25'>{0}</td>", item);
            }
            sbHtml.Append("</tr>");

            int i = 1;

            foreach (var info in result)
            {
                sbHtml.Append("<tr>");
                sbHtml.AppendFormat("<td style='font-size: 12px;height:20px;'>{0}</td>", i);
                sbHtml.AppendFormat("<td style='font-size: 12px;height:20px;'>{0}</td>", info.Workshop);
                sbHtml.AppendFormat("<td style='font-size: 12px;height:20px;'>{0}</td>", info.Name);
                sbHtml.AppendFormat("<td style='font-size: 12px;height:20px;'>{0}</td>", info.ProtectUnit);
                sbHtml.AppendFormat("<td style='font-size: 12px;height:20px;'>{0}</td>", info.IdentifyStyle);
                sbHtml.AppendFormat("<td style='font-size: 12px;height:20px;'>{0}</td>", info.DeviceType);
                sbHtml.AppendFormat("<td style='font-size: 12px;height:20px;'>{0}</td>", info.FactoryCode);
                sbHtml.AppendFormat("<td style='font-size: 12px;height:20px;'>{0}</td>", info.DeviceAccuracy);
                sbHtml.AppendFormat("<td style='font-size: 12px;height:20px;'>{0}</td>", info.Factory);
                sbHtml.AppendFormat("<td style='font-size: 12px;height:20px;'>{0}</td>", info.Cycle);
                sbHtml.AppendFormat("<td style='font-size: 12px;height:20px;'>{0}</td>", info.BuyDate.HasValue ? info.BuyDate.Value.ToString("yyyy-MM-dd") : "");
                sbHtml.AppendFormat("<td style='font-size: 12px;height:20px;'>{0}</td>", info.IdentifyDate.HasValue ? info.IdentifyDate.Value.ToString("yyyy-MM-dd") : "");
                sbHtml.AppendFormat("<td style='font-size: 12px;height:20px;'>{0}</td>", info.ValidDate.HasValue ? info.ValidDate.Value.ToString("yyyy-MM-dd") : "");
                sbHtml.AppendFormat("<td style='font-size: 12px;height:20px;'>{0}</td>", info.IdentifyUnit);
                sbHtml.AppendFormat("<td style='font-size: 12px;height:20px;'>{0}</td>", info.Status);
                sbHtml.AppendFormat("<td style='font-size: 12px;height:20px;'>{0}</td>", info.IdentifyCertificateNo);
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
