using EP.Core.DTOs.AdminPanelViewModels;
using EP.Core.Interfaces.Admin;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EP.Web.Pages.Admin.Roles
{
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

        }

        public IActionResult OnPost(List<int> permissions)
        {



            _adminServices.AddRole(Role, permissions);

            return RedirectToPage("Index");

        }
    }
}
