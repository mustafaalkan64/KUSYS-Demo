using KUSYS_Demo.Core.Models;
using KUSYS_Demo.Infastructure;
using KUSYS_Demo.UI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace KUSYS_Demo.UI.Controllers
{
    public class AccountController : BaseController
    {
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager) : base(userManager, signInManager, roleManager)
        {
        }

        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(UserLoginModel userLoginModel)
        {
            if (ModelState.IsValid)
            {
                AppUser user = await userManager.FindByEmailAsync(userLoginModel.Email);
                if (user != null)
                {

                    Microsoft.AspNetCore.Identity.SignInResult result = await signInManager.PasswordSignInAsync(user, userLoginModel.Password, userLoginModel.RememberMe, false);

                    if (result.Succeeded)
                    {

                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Password is Wrong");
                        ViewBag.Message = "Password is wrong";
                        return View(userLoginModel);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "No registered user found for this email address.");
                    ViewBag.Message = "No registered user found for this email address.";
                }
            }

            return View(userLoginModel);
        }


        public IActionResult SignUp()
        {
            return View();
        }


        public IActionResult LogOut()
        {
            signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }


        public IActionResult AccessDenied()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> SignUp(UserRegisterModel userRegisterModel)
        {
            if (ModelState.IsValid)
            {
                if (userManager.Users.Any(u => u.Email == userRegisterModel.Email))
                {
                    ModelState.AddModelError("", "Email addreess is already taken");
                    ViewBag.Message = "Email addreess is already taken";
                    return View(userRegisterModel);
                }

                if (userManager.Users.Any(u => u.PhoneNumber == userRegisterModel.PhoneNumber))
                {
                    ModelState.AddModelError("", "Phone is already registered");
                    ViewBag.Message = "Phone is already registered";
                    return View(userRegisterModel);
                }

                AppUser user = new AppUser();
                user.UserName = userRegisterModel.UserName;
                user.Email = userRegisterModel.Email;
                user.PhoneNumber = userRegisterModel.PhoneNumber;

                IdentityResult result = await userManager.CreateAsync(user, userRegisterModel.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Message = "Kayıt Sırasında Hata ile Karşılaşıldı";
                    AddModelError(result);
                }
            }

            return View(userRegisterModel);
        }


        public ActionResult ForgetPassword()
        {
            ViewBag.Success = false;
            return View();
        }

        [HttpPost]
        public ActionResult ForgetPassword(UserLoginModel userLoginModel)
        {
            //Add reset password logic here
            throw new NotImplementedException();
        }
    }
}
