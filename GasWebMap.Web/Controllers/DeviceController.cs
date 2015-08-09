using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GasWebMap.Core;
using GasWebMap.Domain;

namespace GasWebMap.Web.Controllers
{
    [Authorize]
    public class DeviceController : Controller
    {
        //
        // GET: /Device/

        public ActionResult Index()
        {
           
            return View();
        }


        public ActionResult Index2()
        {
            return View("index2");
        }

        public ActionResult ImageView()
        {
            var id = this.Request.QueryString["id"];
            var add = this.Request.QueryString.Get("add");
            ViewBag.ID = id;
            ViewBag.CanAdd = add;
            //var deviceID = Guid.Parse(id);
            //var rep = AppEx.Container.GetRepository<ImageStore>();
            //var lst = rep.GetEntities(t => t.DeviceID == deviceID);
            //ViewBag.Data=lst.ToList();

            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult Upload(HttpPostedFileBase fileData)
        {
            if (fileData != null)
            {
                try
                {
                   
                    // 文件上传后的保存路径
                    string filePath = Server.MapPath("~/Files/");
                    if (!Directory.Exists(filePath))
                    {
                        Directory.CreateDirectory(filePath);
                    }
                    string fileName = Path.GetFileName(fileData.FileName);// 原始文件名称
                    string fileExtension = Path.GetExtension(fileName); // 文件扩展名
                    string saveName = Guid.NewGuid().ToString() + fileExtension; // 保存文件名称

                    fileData.SaveAs(filePath + saveName);

                    return Json(new { Success = true, FileName = fileName, SaveName = saveName });
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

    }
}
