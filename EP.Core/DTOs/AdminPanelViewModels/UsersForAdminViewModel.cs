using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP.Core.DTOs.AdminPanelViewModels
{
    public class UsersForAdminViewModel
    {
        public List<Domain.Entities.User.User> Users { get; set; }

        public int PageCount { get; set; }

        public int CurrentId { get; set; }
    }
}
