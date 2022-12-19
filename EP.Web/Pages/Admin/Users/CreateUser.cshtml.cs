using EP.Core.DTOs.AdminPanelViewModels;
using EP.Core.Interfaces.Admin;
using EP.Core.Tools.Security;
using EP.Domain.Interfaces.Roles;
using EP.Domain.Interfaces.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace EP.Web.Pages.Admin.Users
{
    [PermissionChecker(3)]
    public class CreateUserModel : PageModel
    {
        private readonly IUserRepository _userRepository;
        private readonly IAdminServices _adminServices;

        public CreateUserModel(IUserRepository userRepository, IAdminServices adminServices)
        {
            _userRepository = userRepository;
            _adminServices = adminServices;
        }

        [BindProperty]
        public CreateUserViewModel CreateUserViewModel { get; set; }

        public void OnGet()
        {
            ViewData["Roles"] = _adminServices.GetAllRoles();
        }

        public IActionResult OnPost(List<int> userRoles)
        {

            if (!ModelState.IsValid)
            {
                ViewData["Roles"] = _adminServices.GetAllRoles();

                return Page();
            }

            int userId = _adminServices.CreateUserFromAdmin(CreateUserViewModel);

            _adminServices.AddRolesToUser(userRoles, userId);

            return Redirect("Admin/Users");
        }
    }
}
