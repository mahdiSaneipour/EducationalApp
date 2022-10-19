using EP.Core.DTOs.AccountViewModels;
using EP.Core.Interfaces.User;
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

        [Route("Register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterUserViewModel register)
        {
            if (!ModelState.IsValid)
            {
                return View(register);
            }

            string email = register.Email;
            string username = register.UserName;

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

            //TODO: Register User

            return View();
        }
    }
}
