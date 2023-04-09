using Demo.BL.Helper;
using Demo.BL.Models;
using Demo.DAL.Extend;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Controllers
{
    //[Authorize]
    public class AccountController : Controller
    {
        #region Fields
        
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        #endregion

        #region Ctor
        public AccountController(UserManager<ApplicationUser> userManager , SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        #endregion

        #region Registeration
        [HttpGet]
        public IActionResult Registeration()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> Registeration(RegisterationVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = new ApplicationUser()
                    {
                        UserName = model.Email,
                        Email = model.Email,
                        IsAgree = model.IsAgree
                    };

                    var result = await userManager.CreateAsync(user, model.Password);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("Login");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                    }
                }
                ViewBag.Check = model + "ModelState Not Valid";
                return View(model);

            }
            catch (Exception ex)
            {
                ViewBag.Check = ex.Message + "Catch Error";

                return View(model);
            }
        }

        #endregion


        #region Login

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await userManager.FindByEmailAsync(model.Email);
                    var result = await signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Invalid Username or Password");
                    }
                }
                return View(model);

        }
            catch (Exception)
            {
                return View(model);
    }
}

        #endregion

        #region Logout
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
        #endregion


        #region ForgetPassword

        [HttpGet]
        public IActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await userManager.FindByEmailAsync(model.Email);
                    if(user != null)
                    {
                        var token = await userManager.GeneratePasswordResetTokenAsync(user);
                        var passwordResetLink = Url.Action("ResetPassword", "Account", new { Email = model.Email, Token = token }, Request.Scheme);
                        MailSender.SendMail(new MailVM { Mail = model.Email, Title = "Reset Password - Migo", Content = passwordResetLink});

                        return RedirectToAction("ConfirmForgetPassword");
                    }
                    TempData["ForgetPasswordMsg"] = "Failed To Send";
                    return RedirectToAction("ForgetPassword");

                }

                TempData["ForgetPasswordMsg"] = "Invalid Data";
                return View(model);

            }
            catch (Exception)
            {
                return View(model);
            }
        }


        [HttpGet]
        public IActionResult ConfirmForgetPassword()
        {
            return View();
        }


        #endregion


        #region ResetPassord

        [HttpGet]
        public IActionResult ResetPassword(string Email, string Token)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await userManager.FindByEmailAsync(model.Email);
                    
                    if(user != null)
                    {
                        var result = await userManager.ResetPasswordAsync(user, model.Token, model.Password);

                        if (result.Succeeded)
                        {

                            return RedirectToAction("ConfirmResetPassword");
                        }

                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }

                        TempData["ResetPasswordMsg"] = "Failed To Reset Password , Try Again";
                        return View(model);
                    }

                    TempData["ResetPasswordMsg"] = "Invalid Data";
                    return View(model);
                }

                TempData["ResetPasswordMsg"] = "Invalid Data";
                return View(model);

            }
            catch (Exception)
            {
                TempData["ResetPasswordMsg"] = "Failed To Reset Password";
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult ConfirmResetPassword()
        {
            return View();
        }

        #endregion


    }
}
