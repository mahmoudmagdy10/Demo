using Demo.BL.Models;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Controllers
{
    public class LanguageController : Controller
    {
        [HttpGet]
        public IActionResult SetLanguage(LanguageVM model)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(model.culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return LocalRedirect(model.returnUrl);
        }
    }
}
