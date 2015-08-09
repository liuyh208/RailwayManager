using System;
using System.Collections.Generic;
using System.IO;
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
using ServiceStack.Common.Web;
using ServiceStack.ServiceInterface;

namespace GasWebMap.Services.Services
{
    public class DeviceService : ServiceBase
    {
        public PageData<DeviceDto> Get(DeviceGet device)
        {
            if (device == null)
            {
                device = new DeviceGet();
            }
            device.Valid();
            IRepository<DeviceInfo> rep = AppEx.Container.GetRepository<DeviceInfo>();

            CustomUserSession sn = this.SessionAs< CustomUserSession>();
           
            //var    result = rep.GetEntities(t =>t.DepartmentID==sn.DepartmentID && t.ValidDate >= device.StartDate && t.ValidDate < device.EndDate);
            bool bl=device.order=="asc";
            IEnumerable<DeviceInfo> result = null;
            
            if (sn.IsAdmin)
            {
                result = rep.GetEntities(t =>  t.ValidDate >= device.StartDate && t.ValidDate < device.EndDate, device.sort, bl);
            }
            else { 
             result = rep.GetEntities(t => t.DepartmentID == sn.DepartmentID && t.ValidDate >= device.StartDate && t.ValidDate < device.EndDate, device.sort, bl);

            }
            
            if (!string.IsNullOrWhiteSpace(device.Name))
            {
                result = result.Where(t => t.Name != null && t.Name.Contains(device.Name));
            }

            if (!string.IsNullOrWhiteSpace(device.WorkShop))
            {

                result = result.Where(t => t.Workshop !=null &&  t.Workshop.Contains(device.WorkShop));
            }
            result = result.OrderBy(t => t.ValidDate);
            var pg= result.Page(device.Page, device.Rows);


//            if (string.IsNullOrWhiteSpace(device.Name))
//            {
//                pg = rep.GetPagedEntities(
//                    t =>t.Workshop==t.Workshop &&  t.ValidDate >= device.StartDate && t.ValidDate < device.EndDate, device.Page,
//                    device.Rows);

//            }
//            else
//            {
//pg=rep.GetPagedEntities(
//                    t => t.BuyDate >= device.StartDate && t.BuyDate < device.EndDate && t.Name.Contains(device.Name), device.Page,
//                    device.Rows);
//            }
           
                
            IList<DeviceDto> lst = Mapper.Map<IList<DeviceInfo>, IList<DeviceDto>>(pg.ToList());
            return new PageData<DeviceDto>(lst, result.Count());
        }

        public ResponseResult Post(DeviceEdit devcInfo)
        {
            DeviceInfo dev = Mapper.Map<DeviceEdit, DeviceInfo>(devcInfo);
            var sn = this.SessionAs<CustomUserSession>();
            dev.DepartmentID = sn.DepartmentID.Value;
            IRepository<DeviceInfo> rep = AppEx.Container.GetRepository<DeviceInfo>();
            try
            {
                rep.Update(dev);
                return ResponseResult.SuccessRes;
            }
            catch (Exception ex)
            {
                return ResponseResult.FailureRes("保存失败");
            }
            
            
        }



        public ResponseResult Post(DeviceAdd devcInfo)
        {
            
            try
            {
                DeviceInfo dev = Mapper.Map<DeviceAdd, DeviceInfo>(devcInfo);
                var sn = this.SessionAs<CustomUserSession>();
                dev.DepartmentID = sn.DepartmentID.Value;
                IRepository<DeviceInfo> rep = AppEx.Container.GetRepository<DeviceInfo>();

                dev.Id = Guid.NewGuid();
                rep.Add(dev);
                return ResponseResult.SuccessRes;
            }
            catch (Exception ex)
            {
                return ResponseResult.FailureRes("保存失败");
            }
            
          
        }

        public void Post(DeviceImage image)
        {
            var d = new ImageStore();
            d.FileName = image.ImageName;
            d.DeviceID = image.DeviceID;
            d.DateTime = DateTime.Now;
            var arr = image.ImageName.Split('.');

            d.Id = Guid.Parse(arr[0]);

            var rep = AppEx.Container.GetRepository<ImageStore>();
            rep.Add(d);
        }

        public ResponseResult Delete(DeleteDevice device)
        {
            
            try
            {
                IRepository<DeviceInfo> rep = AppEx.Container.GetRepository<DeviceInfo>();
                rep.DeleteByIDs(device);
                return ResponseResult.SuccessRes;
            }
            catch (Exception ex)
            {
                
                throw;
            }
        }

        public void Delete(DeviceImage image) {
            var rep = AppEx.Container.GetRepository<ImageStore>();
            rep.DeleteByID(image.DeviceID);
        }

        public IList<ImageStore> Get(DeviceImage image)
        {
            var rep = AppEx.Container.GetRepository<ImageStore>();
            var lst= rep.GetEntities(t => t.DeviceID == image.DeviceID);
            return lst.ToList();
        }
        public IList<ImageStore> Get(DeleteImage image)
        {
            var rep = AppEx.Container.GetRepository<ImageStore>();
            rep.Delete(t=>t.DeviceID==image.DeviceID && t.FileName==image.ImageID);
            var lst = rep.GetEntities(t => t.DeviceID == image.DeviceID);
            return lst.ToList();
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
                sbHtml.AppendFormat("<td style='font-size: 12px;height:20px;'>{0}</td>", info.BuyDate.HasValue?info.BuyDate.Value.ToString("yyyy-MM-dd"):"");
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
            var fileStream= new MemoryStream(fileContents);
            return new ExcelResult(fileStream);
        }
    }
}