using EP.Core.Interfaces.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EP.Core.Tools.Security
{
    public class PermissionCheckerAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        private int _permissionId = 0;
        public PermissionCheckerAttribute(int permissionId)
        {
            _permissionId = permissionId;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                IAdminServices _adminServices = (IAdminServices) context.HttpContext.RequestServices.GetService(typeof(IAdminServices));

                int userId = Int32.Parse(context.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);

                if (!_adminServices.CheckPermission(_permissionId,userId))
                {
                    context.Result = new RedirectResult("/Login");
                }

            }
            else
            {
                context.Result = new RedirectResult("/Login");
            }
        }
    }
}
