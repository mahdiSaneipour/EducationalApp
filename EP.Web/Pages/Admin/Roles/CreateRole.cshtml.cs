using EP.Core.DTOs.AdminPanelViewModels;
using EP.Core.Interfaces.Admin;
using EP.Core.Tools.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EP.Web.Pages.Admin.Roles
{
    [PermissionChecker(7)]
    public class CreateRoleModel : PageModel
    {
        private readonly IAdminServices _adminServices;

        public CreateRoleModel(IAdminServices adminServices)
        {
            _adminServices = adminServices;
        }

        [BindProperty]
        public CreateRoleViewModel Role { get; set; }

        public void OnGet()
        {
            Console.WriteLine(_adminServices.GetAllPermissions()[0].PermissionName);
            ViewData["Permissions"] = _adminServices.GetAllPermissions();
        }

        public IActionResult OnPost(List<int> selectedPermissions)
        {
            _adminServices.AddRole(Role, selectedPermissions);

            return RedirectToPage("Index");

        }
    }
}
