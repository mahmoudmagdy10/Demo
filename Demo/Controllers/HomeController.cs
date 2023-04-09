using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Controllers
{
    [Authorize(Roles = "Admin,User")]

    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
