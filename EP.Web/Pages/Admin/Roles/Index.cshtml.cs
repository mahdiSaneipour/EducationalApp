using EP.Core.Interfaces.Admin;
using EP.Core.Tools.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EP.Web.Pages.Admin.Roles
{
    [PermissionChecker(6)]
    public class IndexModel : PageModel
    {
        private readonly IAdminServices _adminServices;

        public IndexModel(IAdminServices adminServices)
        {
            _adminServices = adminServices;
        }

        public List<Domain.Entities.User.Role> Roles { get; set; }

        public void OnGet()
        {
            Roles = _adminServices.GetAllRoles();
        }
    }
}
