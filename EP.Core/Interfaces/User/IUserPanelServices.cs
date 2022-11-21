using EP.Core.DTOs.UserPanelViewModels;
using EP.Domain.Interfaces.User;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP.Core.Interfaces.User
{
    public interface IUserPanelServices
    {
        public UserPanelViewModel GetUserInformationForUserPanel(string userId);
    }
}
