using HMS.Areas.Dashboard.ViewModels;
using HMS.Entities;
using HMS.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HMS.Areas.Dashboard.Controllers
{
    public class AccomodationTypesController : Controller
    {
        AccomodationTypesService accomodationTypesService = new AccomodationTypesService();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Listing()
        {
            AccomodationTypesListingModel model = new AccomodationTypesListingModel();

            model.AccomodationTypes = accomodationTypesService.GetAllAccomodationTypes();

            return PartialView("_Listing", model);
        }

        [HttpGet]
        public ActionResult Action()
        {
            AccomodationTypeActionModel model = new AccomodationTypeActionModel();

            return PartialView("_Action", model);
        }

        [HttpPost]
        public JsonResult Action(AccomodationTypeActionModel model)
        {
            JsonResult json = new JsonResult();

            AccomodationType accomodationType = new AccomodationType();

            accomodationType.Name = model.Name;
            accomodationType.Description = model.Description;

            var result = accomodationTypesService.SaveAccomodationType(accomodationType);

            if(result)
            {
                json.Data = new { Success = true };
            }
            else
            {
                json.Data = new { Success = false, Message = "Unable to add Accomodation Type." };
            }

            return json;
        }
    }
}