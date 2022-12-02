using EP.Core.DTOs.UserPanelViewModels;
using EP.Core.Enums.UserPanel;
using EP.Core.Interfaces.User;
using EP.Core.JsonModel.UserPanel;
using EP.Core.ServiceModels.UserPanel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
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

        [HttpPost]
        public IActionResult UploadAvatarAndDeletePreviousOne(IFormFile avatar, string previousAvatar)
        {
            ChangeAvatarServiceModel model = new ChangeAvatarServiceModel();

            model = _userPanelServices.UploadImageAndDeletePreviousOne(avatar, previousAvatar);

            ChangeAvatarEnums status = model.Status;
            string avatarAddress = model.AvatarAddress;

            ResultUploadAvatarJsonModel result = new ResultUploadAvatarJsonModel();

            switch (status)
            {

                case ChangeAvatarEnums.Successful:

                    result = SetResultUploadAvatarJsonModel(avatarAddress,"successful");

                    break;

                case ChangeAvatarEnums.AvatarFileIsNull:

                    result = SetResultUploadAvatarJsonModel(avatarAddress, "avatarFileIsNull");

                    break;

                case ChangeAvatarEnums.PreviousAvatarNotFound:

                    result = SetResultUploadAvatarJsonModel(avatarAddress, "previousAvatarNotFound");

                    break;

                case ChangeAvatarEnums.ServerError:

                    result = SetResultUploadAvatarJsonModel(avatarAddress, "serverError");

                    break;

                default:

                    return BadRequest();
            }

            return new JsonResult(result);

        }

        private ResultUploadAvatarJsonModel SetResultUploadAvatarJsonModel (string avatarAddress, string status) {

            ResultUploadAvatarJsonModel result = new ResultUploadAvatarJsonModel();

            result.avatarAddress = avatarAddress;
            result.status = status;


            return result;
        }
    }
}