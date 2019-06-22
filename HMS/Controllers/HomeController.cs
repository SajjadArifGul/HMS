using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HMS.ViewModels;
using HMS.Services;

namespace HMS.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            HomeViewModel model = new HomeViewModel();

            AccomodationTypesService service = new AccomodationTypesService();

            model.AccomodationTypes = service.GetAllAccomodationTypes();

            return View(model);
        }
    }
}