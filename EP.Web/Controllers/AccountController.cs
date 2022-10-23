using EP.Core.DTOs.AccountViewModels;
using EP.Core.Interfaces.User;
using EP.Core.ServiceModels.Account;
using EP.Core.Tools.FixTexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace EP.Web.Controllers
{
    public class AccountController : Controller
    {

        private readonly IUserServices _userServices;

        public AccountController(IUserServices userServices)
        {
            _userServices = userServices;
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

            //TODO ccreate authorization

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
                        //TODO LOGIN
                        return Redirect("/");
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
    }
}