using EP.Core.DTOs.AdminPanelViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP.Core.Interfaces.Admin
{
    public interface IAdminServices
    {
        UsersForAdminViewModel GetUsersForAdminServices(int currentPage = 1, string filterEmail = "", string filterUsername = "");
    }
}
