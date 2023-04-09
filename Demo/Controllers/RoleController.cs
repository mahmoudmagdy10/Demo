using Demo.BL.Interface;
using Demo.BL.Models;
using Demo.DAL.Extend;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Demo.Controllers
{
    public class RoleController : Controller
    {

        private readonly IRolesRep role;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;

        public RoleController(IRolesRep role, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            this.role = role;
            this.roleManager = roleManager;
            this.userManager = userManager;
        }
        public IActionResult Index()
        {
            var roles = role.Get();
            return View(roles);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(IdentityRole model)
        {
            try
            {
                var RoleModel = await role.Create(model);

                if(RoleModel != null)
                {
                    return RedirectToAction("Index");
                }

                ViewBag.CreateRole = "Failed To Creat";
                return View(model);
            }
            catch (Exception)
            {
                ViewBag.CreateRole = "Invalid Data";
                return View(model);
            }
        }
        [HttpGet]
        public async Task<IActionResult> Edit(string Id)
        {
            try
            {
                var RoleModel = await role.GetById(Id);
                return View(RoleModel);
            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }
        }

        //[HttpPost]
        //public async Task<IActionResult> Edit(IdentityRole model)
        //{
        //    try
        //    {
        //       await role.Edit(model);
        //        return RedirectToAction("Index");
        //    }
        //    catch (Exception)
        //    {
        //        ViewBag.CreateRole = "Invalid Data";
        //        return View(model);
        //    }
        //}

        [HttpGet]
        public async Task<IActionResult> AddOrRemoveUsers(string RoleId)
        {
            ViewBag.RoleId = RoleId;
            var model = await role.AddOrRemoveUsers(RoleId);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddOrRemoveUsers(List<UserInRoleVM> users, string RoleId)
        {
            await role.EditUserInRole(users, RoleId);
            return RedirectToAction("Edit", new { id = RoleId });
        }
        
        [HttpGet]
        public async Task<IActionResult> Details(string Id)
        {
            try
            {
                var RoleModel = await role.GetById(Id);
                return View(RoleModel);
            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }
        }

        public async Task<IActionResult> Delete(IdentityRole model)
        {
            try
            {
                await role.Delete(model);

                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                ViewBag.CreateRole = "Invalid Data";
                return View(model);
            }
        }
    }
}
