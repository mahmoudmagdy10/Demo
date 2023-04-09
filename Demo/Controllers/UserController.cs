using Demo.BL.Interface;
using Demo.BL.Repository;
using Demo.DAL.Extend;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Controllers
{
    public class UserController : Controller
    {
        #region Fields 

        private readonly IUserRep user;
        private readonly Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> userManager;

        #endregion

        #region Ctor 

        public UserController(IUserRep user, UserManager<ApplicationUser> userManager)
        {
            this.user = user;
            this.userManager = userManager;
        }

        #endregion

        #region Actions 

        public IActionResult Index()
        {
            var users = user.Get();
            return View(users);
        }
        public async Task<IActionResult> Details(string id)
        {
            var model = await user.GetById(id);
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var model = await user.GetById(id);
            return View(model);
        }        
        
        [HttpPost]
        public async Task<IActionResult> Edit(ApplicationUser obj)
        {
            try
            {
                await user.Edit(obj);
                if (ModelState.IsValid)
                {
                    //var model =user.Edit(obj);
                    return RedirectToAction("index");
                }
                else
                {
                    TempData["UserUpdate"] = "Failded To Update";
                    return View(obj);
                }

            }
            catch (Exception)
            {
                TempData["UserUpdate"] = "Invalid Data";
                return View(obj);

            }
        }
        public async Task<IActionResult> Delete(ApplicationUser obj)
        {
            try
            {
                await user.Delete(obj);

                if (ModelState.IsValid)
                {
                    return RedirectToAction("index");
                }
                else
                {
                    TempData["UserUpdate"] = "Failded To Delete";
                    return View();
                }

            }
            catch (Exception)
            {
                TempData["UserUpdate"] = "Invalid Data";
                return View();

            }
        }

        #endregion
    }
}
