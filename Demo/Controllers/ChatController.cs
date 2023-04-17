using Demo.BL.Interface;
using Demo.DAL.Entity;
using Demo.DAL.Extend;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Demo.Controllers
{
    public class ChatController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IUserRep user;

        public ChatController(UserManager<ApplicationUser> userManager , IUserRep user)
        {
            this.userManager = userManager;
            this.user = user;
        }

        public IActionResult Index()
        {
            //ViewBag.UsersList = user.Get();
            //ViewBag.User = User.Identity.Name;
            return View();
        }
    }
}
