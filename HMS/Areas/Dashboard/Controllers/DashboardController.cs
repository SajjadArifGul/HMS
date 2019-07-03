using HMS.Entities;
using HMS.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HMS.Areas.Dashboard.Controllers
{
    public class DashboardController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult UploadPictures()
        {
            JsonResult result = new JsonResult();

            var dashboardService = new DashboardService();
            var picsList = new List<Picture>();

            var files = Request.Files;

            for (int i = 0; i < files.Count; i++)
            {
                var picture = files[i];

                var fileName = Guid.NewGuid() + Path.GetExtension(picture.FileName);
                var filePath = Server.MapPath("~/images/site/") + fileName;

                picture.SaveAs(filePath);

                var dbPicture = new Picture();
                dbPicture.URL = fileName;
                
                if(dashboardService.SavePicture(dbPicture))
                {
                    picsList.Add(dbPicture);
                }
            }

            result.Data = picsList;

            return result;
        }
    }
}