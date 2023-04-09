using Demo.BL.Models;
using Demo.BL.Helper;
using Microsoft.AspNetCore.Mvc;
namespace Demo.Controllers
{
    public class MailController : Controller
    {
        [HttpGet]
        public IActionResult Send()
        {
            return View();
        }        
        
        [HttpPost]
        public IActionResult Send(MailVM model)
        {
            try
            {
                TempData["Msg"] = MailSender.SendMail(model);
                return View();
            }
            catch(Exception ex)
            {
                TempData["Msg"] = MailSender.SendMail(model);
                return View();
            }

        }
    }
}
