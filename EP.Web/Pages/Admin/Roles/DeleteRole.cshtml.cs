using EP.Core.DTOs.AdminPanelViewModels;
using EP.Core.Interfaces.Admin;
using EP.Core.Tools.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EP.Web.Pages.Admin.Roles
{
    [PermissionChecker(12)]
    public class DeleteRoleModel : PageModel
    {
        private readonly IAdminServices _adminServices;

        public DeleteRoleModel(IAdminServices adminServices)
        {
            _adminServices = adminServices;
        }

        [BindProperty]
        public EditRoleViewModel Role { get; set; }

        public void OnGet(int roleId)
        {
            Role = _adminServices.GetRoleByRoleId(roleId);
        }

        public IActionResult OnPost()
        {

            _adminServices.RemoveRole(Role);

            return RedirectToPage("Index");
        }

    }
}
