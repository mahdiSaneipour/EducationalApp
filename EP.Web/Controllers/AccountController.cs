using EP.Core.DTOs.AccountViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EP.Web.Controllers
{
    public class AccountController : Controller
    {
        [Route("Register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterUserViewModel register)
        {
            return View();
        }
    }
}
