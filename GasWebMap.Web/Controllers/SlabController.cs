using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using GasWebMap.Core;
using GasWebMap.Domain;
using GasWebMap.Services.Dtos;
using GasWebMap.Services.Host;
using GasWebMap.Services.Responses;
using GasWebMap.Services.Services;
using NPOI.HSSF.UserModel;
using NPOI.SS.Formula.Functions;
using NPOI.SS.UserModel;
using NPOI.XWPF.UserModel;
using ServiceStack.CacheAccess;
using ServiceStack.Common;
using ServiceStack.ServiceInterface;

namespace GasWebMap.Web.Controllers
{
       [Authorize]
    public class SlabController : Controller
    {
        //
        // GET: /Slab/

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetData(SlabGet request)
        {
            var rep = AppEx.Container.GetRepository<SlabProblem>();
            var s = Session;
            bool bl = request.order == "asc";

            Expression<Func<SlabProblem, bool>> filter = t => t.DepartmentID == ServiceInit.CustomSession.DepartmentID;
            if (!string.IsNullOrWhiteSpace(request.RailWay))
            {
                filter = filter.And(t => t.RailWay == request.RailWay);
            }

            if (!string.IsNullOrWhiteSpace(request.RunType))
            {
                filter = filter.And(t => t.RunType == request.RunType);
            }
            var result = rep.GetPagedEntities(filter, t => t.CheckDate, bl, request.Page, request.Rows);

            var r= new PageData<SlabProblem>(result.Result, result.Total);
            return Json(r, JsonRequestBehavior.AllowGet);
        }


        public JsonResult Delete(List<Guid> request)
        {

            try
            {
                var rep = AppEx.Container.GetRepository<SlabProblem>();
                rep.DeleteByIDs(request);
                return Json(new { Success = true }, JsonRequestBehavior.AllowGet);
                //return Content("", "text/html;charset=UTF-8");
            }
            catch (Exception ex)
            {
                return Json(new { Success = false, Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }


       // [HttpPost]
        public ActionResult Add(SlabProblem slab)
        {
            try
            {
                var rep = AppEx.Container.GetRepository<SlabProblem>();
                slab.Id = Guid.NewGuid();

                slab.DepartmentID = ServiceInit.CustomSession.DepartmentID;

                rep.Add(slab);
                return Content("", "text/html;charset=UTF-8");
            }
            catch (Exception ex)
            {
                return Content("添加失败", "text/html;charset=UTF-8");
            }
            
            
           
        }

        public ActionResult Edit(SlabProblem slab)
        {
            try
            {
                var rep = AppEx.Container.GetRepository<SlabProblem>();

                rep.Update(slab);
                return Content("", "text/html;charset=UTF-8");
            }
            catch (Exception ex)
            {
                return Content("添加失败", "text/html;charset=UTF-8");
            }



        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Upload(HttpPostedFileBase fileData)
        {
            if (fileData != null)
            {
                try
                {
                    // 文件上传后的保存路径
                    string filePath = Server.MapPath("~/Temp/");
                    if (!Directory.Exists(filePath))
                    {
                        Directory.CreateDirectory(filePath);
                    }
                    string fileName = Path.GetFileName(fileData.FileName);// 原始文件名称
                    string fileExtension = Path.GetExtension(fileName); // 文件扩展名
                    string saveName =filePath + Guid.NewGuid().ToString() + fileExtension; // 保存文件名称

                    fileData.SaveAs( saveName);
                    var bl=  ImportData(saveName);
                    if (bl)
                    {
                        return Content("", "text/html;charset=UTF-8");
                    }
                    else
                    {
                        return Content("导入失败", "text/html;charset=UTF-8");
                    }
                    
                }
                catch (Exception ex)
                {
                    return Content("导入失败", "text/html;charset=UTF-8");
                }
            }
            else
            {
                return Content("请选择要上传的文件", "text/html;charset=UTF-8");
            }
        }

        private bool ImportData(string xls)
        {
            
            using (FileStream stream = System.IO.File.OpenRead(xls))
            {
                var startIndex = 4;
                HSSFWorkbook book = new HSSFWorkbook(stream);
                var sheet = book.GetSheetAt(0);
                var wk = sheet.GetRow(1).GetCell(2).StringCellValue;
                var lst=new List<SlabProblem>();

                using (var service = AppHost.ResolveService<SlabProblemService>(System.Web.HttpContext.Current))
                {
                     
                    var session = (CustomUserSession)service.GetSession();
                    while (startIndex <= sheet.LastRowNum)
                    {
                        var row = sheet.GetRow(startIndex);
                        var slab = Row2Slab(row);
                        slab.Workshop = wk;
                        slab.DepartmentID = session.DepartmentID;
                        lst.Add(slab);
                        startIndex++;
                    }

                    var rep = AppEx.Container.GetRepository<SlabProblem>();
                    rep.Add(lst);
                }
                return true;

                //SlabProblemService2 service=new SlabProblemService2();
                //var s= service.Add(lst);
                //return string.IsNullOrEmpty(s);
            }
            return false;
        }

        private SlabProblem Row2Slab(IRow row)
        {
            SlabProblem item = null;
            try
            {
                item=new SlabProblem();
                item.Id = Guid.NewGuid();
                item.RailWay = GetCellString(row.GetCell(1));
                item.RunType = GetCellString(row.GetCell(2));
                item.Mileage = GetCellDouble(row.GetCell(3));
                item.StartBoardID = GetCellString(row.GetCell(4));
                item.SupportRailID = GetCellString(row.GetCell(5));
                item.DeviceDesc = GetCellString(row.GetCell(6));
                item.HurtItem = GetCellString(row.GetCell(7));
                item.HurtPosition = GetCellString(row.GetCell(8));
                item.HurtStyle = GetCellString(row.GetCell(9));
                item.Length = GetCellDouble(row.GetCell(10));
                item.Width = GetCellDouble(row.GetCell(11));
                item.Height = GetCellDouble(row.GetCell(12));
                item.HurtLevel = GetCellString(row.GetCell(13));
                item.CheckDate = GetCellDate(row.GetCell(14));
                item.Checker = GetCellString(row.GetCell(15));
                item.Movie = GetCellString(row.GetCell(16));
                item.TakeMeasures = GetCellString(row.GetCell(17));
                item.LogoutDate = GetCellDate(row.GetCell(18));
                item.LogoutMan = GetCellString(row.GetCell(19));
                item.Remark = GetCellString(row.GetCell(20));

            }
            catch (Exception ex) 
            {
                
                throw;
            }
            return item;
        }

        private string GetCellString(ICell cell)
        {
            switch (cell.CellType)
            {
                case CellType.String:
                    return cell.StringCellValue;
                    break;
                case CellType.Numeric:
                    return cell.NumericCellValue.ToString();
                    break;
                case CellType.Boolean:
                    return cell.BooleanCellValue.ToString();
                case CellType.Blank:
                    return "";
                default:
                    return cell.StringCellValue;
            }
        }
        private double GetCellDouble(ICell cell)
        {
            bool bl = false;
            double v = 0.0;
            switch (cell.CellType)
            {
                case CellType.String:
                  bl=  double.TryParse(cell.StringCellValue, out v);
                    return v;
                    break;
                case CellType.Numeric:
                    return cell.NumericCellValue;
                    break;
                case CellType.Boolean:
                    return cell.BooleanCellValue?1:0;
                case CellType.Blank:
                    return v;
                default:
                    return v;
            }
        }

        private DateTime GetCellDate(ICell cell)
        {
            var dt = DateTime.MinValue;
            switch (cell.CellType)
            {
                case CellType.String:
                    var bl=DateTime.TryParse(cell.StringCellValue,out dt) ;
           
                    break;
                case CellType.Numeric:
                    return cell.DateCellValue;
                    break;
               
                case CellType.Blank:
                default:
                    return dt;
            }
            return dt;
        }
    }
}
