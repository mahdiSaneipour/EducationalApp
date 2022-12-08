using EP.Core.DTOs.UserPanelViewModels;
using EP.Core.Enums.UserPanel;
using EP.Core.Interfaces.User;
using EP.Core.JsonModel.UserPanel;
using EP.Core.ServiceModels.UserPanel;
using EP.Web.Models;
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

            return View("UserPanel",result);
        }

        [Route("UserPanel/EditProfile")]
        public IActionResult EditProfile()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            EditProfileViewModel data = new EditProfileViewModel();

            data = _userPanelServices.GetEditProfileInformation(userId);

            return View(data);
        }

        [HttpPost]
        [Route("UserPanel/EditProfile")]
        public IActionResult EditProfile(EditProfileViewModel profile)
        {

            if (!ModelState.IsValid)
            {
                return View();
            }

            ChangeProfileEnums result = _userPanelServices.EditProfileByUserPanel(profile);
            EditProfileViewModel data = new EditProfileViewModel();

            if (result != ChangeProfileEnums.Successful)
            {
                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                data = _userPanelServices.GetEditProfileInformation(userId);
            }

            if (result == ChangeProfileEnums.UsernameIsRepetitious)
            {
                ModelState.AddModelError("Username"," قبلا انتخاب شده است" + profile.Username);
                return View(data);
            }

            if (result == ChangeProfileEnums.PreviousImageNotFound)
            {
                ModelState.AddModelError("Username", "آواتار قبلی برای حذف پیدا نشد");
                return View(data);
            }

            if(result == ChangeProfileEnums.ServerError)
            {
                ModelState.AddModelError("Username", "مشکلی در سیستم پیش آمده است");
                return View(data);
            }

            return Redirect("/UserPanel");
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

        [Route("UserPanel/ChangePassword")]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [Route("UserPanel/ChangePassword")]
        public IActionResult ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            model.userId = Int32.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            ChangePasswordEnums result = _userPanelServices.ChangePassword(model);

            Console.WriteLine("result : " + result);

            if (result == ChangePasswordEnums.CurrentPasswordIsNotTrue)
            {
                ModelState.AddModelError("CurrentPassword","رمزعبور فعلی صحیح نمیباشد");
                return View();
            }
            else if (result == ChangePasswordEnums.ServerError)
            {
                ModelState.AddModelError("CurrentPassword", "با عرز پوزش خطایی در سیستم رخ داده, لطفا بعدا تلاش کنید");
                return View();
            }

            return Redirect("/UserPanel");
        }
    }
}