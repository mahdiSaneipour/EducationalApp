using EP.Core.DTOs.AdminPanelViewModels;
using EP.Core.Interfaces.Admin;
using EP.Core.Tools.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EP.Web.Pages.Admin.Roles
{
    [PermissionChecker(8)]
    public class EditRoleModel : PageModel
    {

        private readonly IAdminServices _adminServices;

        public EditRoleModel(IAdminServices adminServices)
        {
            _adminServices = adminServices;
        }

        [BindProperty]
        public EditRoleViewModel Role { get; set; }

        public void OnGet(int roleId)
        {
            Role = _adminServices.GetRoleByRoleId(roleId);
            ViewData["Permissions"] = _adminServices.GetAllPermissions();
            ViewData["RolePermissions"] = _adminServices.GetRolePermissionsByRoleId(roleId);
        }

        public IActionResult OnPost(List<int> selectedPermissions)
        {
            _adminServices.EditRole(Role, selectedPermissions);

            return RedirectToPage("Index");
        }
    }
}
