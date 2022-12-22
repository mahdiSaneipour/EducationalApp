using EP.Core.Enums.UserPanel;
using EP.Core.Interfaces.Course;
using EP.Core.JsonModel.UserPanel;
using EP.Core.ServiceModels.UserPanel;
using EP.Domain.Interfaces.Course;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EP.Web.Api.Admin
{
    public class ApiAdminController : Controller
    {
        private readonly ICourseServices _courseService;

        public ApiAdminController(ICourseServices courseService)
        {
            _courseService = courseService;
        }

        [Route("Api/Admin/GetCourseGroupsByParentId/{parentId}")]
        public IActionResult GetCourseGroupsByParentId(int parentId)
        {
            SelectList reslut = _courseService.GetCourseGroupsByParentIdAsSelectList(parentId);
            return Json(reslut);
        }

        [HttpPost]
        [Route("Api/Admin/UploadImageCourseAndDeletePreviousOne")]
        public IActionResult UploadImageCourseAndDeletePreviousOne(IFormFile avatar, string previousCourseImage)
        {
            ChangeAvatarServiceModel model = new ChangeAvatarServiceModel();

            model = _courseService.UploadImageCourseAndDeletePreviousOne(avatar, previousCourseImage);

            ChangeAvatarEnums status = model.Status;
            string avatarAddress = model.AvatarAddress;

            ResultUploadAvatarJsonModel result = new ResultUploadAvatarJsonModel();

            switch (status)
            {

                case ChangeAvatarEnums.Successful:

                    result = SetResultUploadAvatarJsonModel(avatarAddress, "successful");

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

        private ResultUploadAvatarJsonModel SetResultUploadAvatarJsonModel(string avatarAddress, string status)
        {

            ResultUploadAvatarJsonModel result = new ResultUploadAvatarJsonModel();

            result.avatarAddress = avatarAddress;
            result.status = status;


            return result;
        }

    }
}
