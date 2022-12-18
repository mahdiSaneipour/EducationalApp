using EP.Core.DTOs.AdminPanelViewModels;
using EP.Core.Interfaces.Admin;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EP.Web.Pages.Admin.Roles
{
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
        }

        public IActionResult OnPost(List<int> permissions)
        {
            _adminServices.EditRole(Role, permissions);

            return RedirectToPage("Index");
        }
    }
}
