using EP.Core.DTOs.AdminPanelViewModels;
using EP.Core.Interfaces.Admin;
using EP.Core.Tools.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EP.Web.Pages.Admin.Users
{
    [PermissionChecker(4)]
    public class DeletedUsersModel : PageModel
    {
        private readonly IAdminServices _adminServices;

        public DeletedUsersModel(IAdminServices adminServices)
        {
            _adminServices = adminServices;
        }


        public UsersForAdminViewModel UsersModel { get; set; }

        public void OnGet(int pageId = 1, string filterUserName = "", string filterEmail = "")
        {
            UsersModel = _adminServices.GetUsersForAdminServices(pageId, filterEmail, filterUserName,true);
        }
    }
}
