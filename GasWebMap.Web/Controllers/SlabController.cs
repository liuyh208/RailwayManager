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
using GasWebMap.Services.Responses;
using GasWebMap.Services.Services;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XWPF.UserModel;

namespace GasWebMap.Web.Controllers
{
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
        
            bool bl = request.order == "asc";

            Expression<Func<SlabProblem, bool>> filter = t => t.Id != null;
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
        public JsonResult Upload(HttpPostedFileBase fileData)
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
                        return Json(new {Success = true});
                    }
                    else
                    {
                        return Json(new { Success = false, Message = "导入数据错误" }, JsonRequestBehavior.AllowGet);
                    }
                    
                }
                catch (Exception ex)
                {
                    return Json(new { Success = false, Message = ex.Message }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {

                return Json(new { Success = false, Message = "请选择要上传的文件！" }, JsonRequestBehavior.AllowGet);
            }
        }

        private bool ImportData(string xls)
        {
            
            using (FileStream stream = System.IO.File.OpenRead(xls))
            {
                var startIndex = 4;
                HSSFWorkbook book = new HSSFWorkbook(stream);
                var sheet = book.GetSheetAt(0);
                var lst=new List<SlabProblem>();
                while (startIndex<=sheet.LastRowNum)
                {
                    var row = sheet.GetRow(startIndex);
                    lst.Add(Row2Slab(row));


                    startIndex++;
                }

                SlabProblemService2 service=new SlabProblemService2();
                var s= service.Add(lst);
                return string.IsNullOrEmpty(s);
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
                item.Mileage = GetCellString(row.GetCell(3));
                item.StartBoardID = GetCellString(row.GetCell(4));
                item.SupportRailID = GetCellString(row.GetCell(5));
                item.HurtItem = GetCellString(row.GetCell(6));
                item.HurtPosition = GetCellString(row.GetCell(7));
                item.HurtStyle = GetCellString(row.GetCell(8));
                item.Length = GetCellDouble(row.GetCell(9));
                item.Width = GetCellDouble(row.GetCell(10));
                item.Height = GetCellDouble(row.GetCell(11));
                item.HurtLevel = GetCellString(row.GetCell(12));
                item.CheckDate = GetCellDate(row.GetCell(13));
                item.Checker = GetCellString(row.GetCell(14));
                item.Movie = GetCellString(row.GetCell(15));
                item.TakeMeasures = GetCellString(row.GetCell(16));
                item.LogoutDate = GetCellDate(row.GetCell(17));
                item.LogoutMan = GetCellString(row.GetCell(18));
                item.Remark = GetCellString(row.GetCell(19));

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

        private string GetCellDate(ICell cell)
        {
            switch (cell.CellType)
            {
                case CellType.String:
                    return cell.StringCellValue;
                    break;
                case CellType.Numeric:
                    return cell.DateCellValue.ToString("yyyy-MM-dd HH:mm:ss");
                    break;
                case CellType.Boolean:
                    return cell.BooleanCellValue.ToString();
                case CellType.Blank:
                    return "";
                default:
                    return cell.StringCellValue;
            }
        }
    }
}
