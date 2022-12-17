using EP.Core.DTOs.AdminPanelViewModels;
using EP.Core.Interfaces.Admin;
using EP.Domain.Entities.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EP.Web.Pages.Admin.Users
{
    public class EditUserModel : PageModel
    {

        private readonly IAdminServices _adminServices;

        public EditUserModel(IAdminServices adminServices)
        {
            _adminServices = adminServices;
        }

        [BindProperty]
        public EditUserAdminViewModel EditUserAdminViewModel { get; set; }

        public void OnGet(int userId)
        {

            EditUserAdminViewModel = _adminServices.GetEditUserViewModel(userId);

            ViewData["Roles"] = _adminServices.GetAllRoles();

        }


        public IActionResult OnPost(List<int> userRoles)
        {

            int result = _adminServices.EditUserFromAdmin(EditUserAdminViewModel, userRoles);

            if(result == 0)
            {
                EditUserAdminViewModel = _adminServices.GetEditUserViewModel(EditUserAdminViewModel.userId);

                ViewData["Roles"] = _adminServices.GetAllRoles();

                return Page();
            }

            return RedirectToPage("Index");
        }
    }
}
