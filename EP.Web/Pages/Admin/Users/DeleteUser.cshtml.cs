using EP.Core.DTOs.UserPanelViewModels;
using EP.Core.Interfaces.Admin;
using EP.Core.Interfaces.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EP.Web.Pages.Admin.Users
{
    public class DeleteUserModel : PageModel
    {
        private readonly IUserPanelServices _userPanelServices;
        private readonly IAdminServices _adminServices;

        public DeleteUserModel(IUserPanelServices userPanelServices, IAdminServices adminServices)
        {
            _userPanelServices = userPanelServices;
            _adminServices = adminServices;
        }

        public UserPanelViewModel User { get; set; }

        public void OnGet(int userId)
        {
            ViewData["userId"] = userId;
            User = _userPanelServices.GetUserInformationForUserPanel(userId.ToString());

        }

        public IActionResult OnPost(int userId)
        {

            _adminServices.DeleteUserByUserId(userId);

            return RedirectToPage("Index");
        }
    }
}
