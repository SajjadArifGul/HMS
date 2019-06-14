using HMS.Areas.Dashboard.ViewModels;
using HMS.Entities;
using HMS.Services;
using HMS.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace HMS.Areas.Dashboard.Controllers
{
    public class RolesController : Controller
    {
        private HMSSignInManager _signInManager;
        private HMSUserManager _userManager;

        public HMSSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<HMSSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }
        public HMSUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<HMSUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public RolesController()
        {
        }

        public RolesController(HMSUserManager userManager, HMSSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }
        
        public ActionResult Index(string searchTerm, int? page)
        {
            int recordSize = 10;
            page = page ?? 1;

            RolesListingModel model = new RolesListingModel();

            model.SearchTerm = searchTerm;

            model.Roles = SearchRoles(searchTerm, page.Value, recordSize);

            var totalRecords = SearchRolesCount(searchTerm);

            model.Pager = new Pager(totalRecords, page, recordSize);

            return View(model);
        }

        public IEnumerable<IdentityRole> SearchRoles(string searchTerm, int page, int recordSize)
        {
            //var users = UserManager.Users.AsQueryable();

            //if (!string.IsNullOrEmpty(searchTerm))
            //{
            //    users = users.Where(a => a.Email.ToLower().Contains(searchTerm.ToLower()));
            //}

            //var skip = (page - 1) * recordSize;

            //return users.OrderBy(x => x.Email).Skip(skip).Take(recordSize).ToList();

            return null;
        }

        public int SearchRolesCount(string searchTerm)
        {
            var users = UserManager.Users.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                users = users.Where(a => a.Email.ToLower().Contains(searchTerm.ToLower()));
            }
            
            return users.Count();
        }

        [HttpGet]
        public async Task<ActionResult> Action(string ID)
        {
            RoleActionModel model = new RoleActionModel();

            if (!string.IsNullOrEmpty(ID)) //we are trying to edit a record
            {
                //var user = await UserManager.FindByIdAsync(ID);

                //model.ID = user.Id;
                //model.FullName = user.FullName;
            }

            return PartialView("_Action", model);
        }

        [HttpPost]
        public async Task<JsonResult> Action(RoleActionModel model)
        {
            JsonResult json = new JsonResult();

            IdentityResult result = null;

            if (!string.IsNullOrEmpty(model.ID)) //we are trying to edit a record
            {
                //var user = await UserManager.FindByIdAsync(model.ID);

                //user.FullName = model.FullName;

                //result = await UserManager.UpdateAsync(user);
            }
            else //we are trying to create a record
            {
                //var user = new HMSUser();

                //user.FullName = model.FullName;

                //result = await UserManager.CreateAsync(user);
            }

            json.Data = new { Success = result.Succeeded, Message = string.Join(", ", result.Errors) };

            return json;
        }

        [HttpGet]
        public async Task<ActionResult> Delete(string ID)
        {
            RoleActionModel model = new RoleActionModel();

            //var user = await UserManager.FindByIdAsync(ID);

            //model.ID = user.Id;

            return PartialView("_Delete", model);
        }

        [HttpPost]
        public async Task<JsonResult> Delete(RoleActionModel model)
        {
            JsonResult json = new JsonResult();

            IdentityResult result = null;

            if (!string.IsNullOrEmpty(model.ID)) //we are trying to delete a record
            {
                //var user = await UserManager.FindByIdAsync(model.ID);

                //result = await UserManager.DeleteAsync(user);

                json.Data = new { Success = result.Succeeded, Message = string.Join(", ", result.Errors) };
            }
            else
            {
                json.Data = new { Success = false, Message = "Invalid user." };
            }

            return json;
        }
    }
}