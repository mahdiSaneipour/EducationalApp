using EP.Core.DTOs.AccountViewModels;
using EP.Core.Enums.UserEnums;
using EP.Core.Interfaces.User;
using EP.Core.ServiceModels.Account;
using EP.Core.Tools.FixTexts;
using EP.Core.Tools.RenderViewToString;
using EP.Domain.Entities.User;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Security.Claims;

namespace EP.Web.Controllers
{
    public class AccountController : Controller
    {

        private readonly IUserAccountServices _userServices;
        private readonly IViewRenderService _viewRenderService;

        public AccountController(
            IUserAccountServices userServices, IViewRenderService viewRenderService)
        {
            _userServices = userServices;
            _viewRenderService = viewRenderService;
        }

        #region Register

        [Route("Register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [Route("Register")]
        public IActionResult Register(RegisterUserViewModel register)
        {
            if (!ModelState.IsValid)
            {
                return View(register);
            }

            string email = register.Email;
            string username = register.UserName;
            int userId = 0;

            if (_userServices.IsEmailExist(FixText.FixEmail(email)))
            {
                ModelState.AddModelError("Email", "این ایمیل قبلا استفاده شده است");
                return View(register);
            }

            if (_userServices.IsUserNameExist(username))
            {
                ModelState.AddModelError("UserName", "این نام کاربری قبلا استفاده شده است");
                return View(register);
            }

            userId = _userServices.AddUser(register);

            return View();
        }

        #endregion

        #region Login

        [Route("Login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login(LoginUserViewModel login)
        {

            if (!ModelState.IsValid)
            {
                return View();
            }

            LoginServiceModel status = _userServices.LoginUser(login);

            if (status.IsUserExist)
            {
                if (status.IsPasswordTrue)
                {
                    if (status.IsActive)
                    {

                        User user = status.User;

                        var claims = new List<Claim>(){
                            new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                            new Claim(ClaimTypes.Name, user.UserName)
                        };

                        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        var principal = new ClaimsPrincipal(identity);

                        var properties = new AuthenticationProperties()
                        {
                            IsPersistent = login.RememberMe
                        };

                        HttpContext.SignInAsync(principal,properties);

                        ViewBag.IsSuccess = true;
                        return View();

                    } else
                    {
                        ModelState.AddModelError("Email", "حساب کاربری فعال نیست");
                    }
                } else
                {
                    ModelState.AddModelError("Password", "رمز عبور اشتباه است");
                }
            } else
            {
                ModelState.AddModelError("Email", "کاربری با این ایمیل پیدا نشد");
            }
            

            return View(login);
        }

        #endregion

        #region Logout

        [Route("Logout")]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }

        #endregion

        #region Active Account

        public IActionResult ActiveAccount(string id)
        {
            ViewBag.IsActive = false;
            
            ActiveUserEnum result = _userServices.SetActiveAccount(id,true);

            if (result == ActiveUserEnum.Successful)
            {
                ViewBag.IsActive = true;
            }

            return View();
        }

        #endregion

        #region Forgot Password

        [Route("ForgotPassword")]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [Route("ForgotPassword")]
        public IActionResult ForgotPassword(ForgotPasswordViewModel forgotPassword)
        {

            if (!ModelState.IsValid)
            {
                return View(forgotPassword);
            }

            ForgotPasswordEnum result = _userServices.ForgotPassword(forgotPassword);

            ViewBag.Result = result;

            return View();
        }

        #endregion

        #region Reset Password

        [Route("ResetPassword/{userCode}")]
        public IActionResult ResetPassword(string userCode)
        {
            return View(new ResetPasswordViewModel()
            {
                UserCode = userCode
            });
        }

        [HttpPost]
        [Route("ResetPassword/{userCode}")]
        public IActionResult ResetPassword(ResetPasswordViewModel resetPasswordViewModel)
        {

            if (!ModelState.IsValid)
            {
                return View();
            }

            ResetPasswordEnums result = _userServices.ResetPassword(resetPasswordViewModel);
            
            ViewBag.Result = result;

            return View();
        }

        #endregion
    }
}