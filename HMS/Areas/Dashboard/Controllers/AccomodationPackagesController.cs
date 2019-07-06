using HMS.Areas.Dashboard.ViewModels;
using HMS.Entities;
using HMS.Services;
using HMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HMS.Areas.Dashboard.Controllers
{
    public class AccomodationPackagesController : Controller
    {
        AccomodationPackagesService accomodationPackagesService = new AccomodationPackagesService();
        AccomodationTypesService accomodationTypesService = new AccomodationTypesService();

        DashboardService dashboardService = new DashboardService();

        public ActionResult Index(string searchTerm, int? accomodationTypeID, int? page)
        {
            int recordSize = 5;
            page = page ?? 1;

            AccomodationPackagesListingModel model = new AccomodationPackagesListingModel();

            model.SearchTerm = searchTerm;
            model.AccomodationTypeID = accomodationTypeID;

            model.AccomodationTypes = accomodationTypesService.GetAllAccomodationTypes();

            model.AccomodationPackages = accomodationPackagesService.SearchAccomodationPackages(searchTerm, accomodationTypeID, page.Value, recordSize);
            var totalRecords = accomodationPackagesService.SearchAccomodationPackagesCount(searchTerm, accomodationTypeID);

            model.Pager = new Pager(totalRecords, page, recordSize);

            return View(model);
        }

        [HttpGet]
        public ActionResult Action(int? ID)
        {
            AccomodationPackageActionModel model = new AccomodationPackageActionModel();

            if (ID.HasValue) //we are trying to edit a record
            {
                var accomodationPackage = accomodationPackagesService.GetAccomodationPackageByID(ID.Value);

                model.ID = accomodationPackage.ID;
                model.AccomodationTypeID = accomodationPackage.AccomodationTypeID;
                model.Name = accomodationPackage.Name;
                model.NoOfRoom = accomodationPackage.NoOfRoom;
                model.FeePerNight = accomodationPackage.FeePerNight;

                model.AccomodationPackagePictures = accomodationPackagesService.GetPicturesByAccomodationPackageID(accomodationPackage.ID);
            }

            model.AccomodationTypes = accomodationTypesService.GetAllAccomodationTypes();

            return PartialView("_Action", model);
        }

        [HttpPost]
        public JsonResult Action(AccomodationPackageActionModel model)
        {
            JsonResult json = new JsonResult();

            var result = false;

            //model.PictureIDs = "90,67,23" = ["90", "67", "23"] = {90, 67, 23}
            List<int> pictureIDs = !string.IsNullOrEmpty(model.PictureIDs) ? model.PictureIDs.Split(',').Select(x => int.Parse(x)).ToList() : new List<int>();
            var pictures = dashboardService.GetPicturesByIDs(pictureIDs);

            if (model.ID > 0) //we are trying to edit a record
            {
                var accomodationPackage = accomodationPackagesService.GetAccomodationPackageByID(model.ID);

                accomodationPackage.AccomodationTypeID = model.AccomodationTypeID;
                accomodationPackage.Name = model.Name;
                accomodationPackage.NoOfRoom = model.NoOfRoom;
                accomodationPackage.FeePerNight = model.FeePerNight;

                accomodationPackage.AccomodationPackagePictures.Clear();
                accomodationPackage.AccomodationPackagePictures.AddRange(pictures.Select(x => new AccomodationPackagePicture() { AccomodationPackageID = accomodationPackage.ID, PictureID = x.ID }));

                result = accomodationPackagesService.UpdateAccomodationPackage(accomodationPackage);
            }
            else //we are trying to create a record
            {
                AccomodationPackage accomodationPackage = new AccomodationPackage();

                accomodationPackage.AccomodationTypeID = model.AccomodationTypeID;
                accomodationPackage.Name = model.Name;
                accomodationPackage.NoOfRoom = model.NoOfRoom;
                accomodationPackage.FeePerNight = model.FeePerNight;

                accomodationPackage.AccomodationPackagePictures = new List<AccomodationPackagePicture>();
                accomodationPackage.AccomodationPackagePictures.AddRange(pictures.Select(x=> new AccomodationPackagePicture() { PictureID = x.ID }));

                result = accomodationPackagesService.SaveAccomodationPackage(accomodationPackage);
            }

            if (result)
            {
                json.Data = new { Success = true };
            }
            else
            {
                json.Data = new { Success = false, Message = "Unable to perform action on Accomodation Package." };
            }

            return json;
        }

        [HttpGet]
        public ActionResult Delete(int ID)
        {
            AccomodationPackageActionModel model = new AccomodationPackageActionModel();

            var accomodationPackage = accomodationPackagesService.GetAccomodationPackageByID(ID);

            model.ID = accomodationPackage.ID;

            return PartialView("_Delete", model);
        }

        [HttpPost]
        public JsonResult Delete(AccomodationPackageActionModel model)
        {
            JsonResult json = new JsonResult();

            var result = false;

            var accomodationPackage = accomodationPackagesService.GetAccomodationPackageByID(model.ID);

            result = accomodationPackagesService.DeleteAccomodationPackage(accomodationPackage);

            if (result)
            {
                json.Data = new { Success = true };
            }
            else
            {
                json.Data = new { Success = false, Message = "Unable to perform action on Accomodation Package." };
            }

            return json;
        }
    }
}