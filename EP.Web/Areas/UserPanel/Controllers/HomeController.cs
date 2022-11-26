using EP.Core.DTOs.UserPanelViewModels;
using EP.Core.Interfaces.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EP.Web.Areas.UserPanel.Controllers
{
    [Authorize]
    [Area("UserPanel")]
    public class HomeController : Controller
    {
        private readonly IUserPanelServices _userPanelServices;

        public HomeController(IUserPanelServices userPanelServices)
        {
            _userPanelServices = userPanelServices;
        }

        public IActionResult Index()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            UserPanelViewModel result = new UserPanelViewModel();

            result = _userPanelServices.GetUserInformationForUserPanel(userId);

            return View("UserPanelIndex",result);
        }

        [Route("UserPanel/EditProfile")]
        public IActionResult EditProfile()
        {
            return View();
        }
    }
}