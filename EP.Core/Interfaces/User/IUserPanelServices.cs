using EP.Core.DTOs.UserPanelViewModels;
using EP.Core.ServiceModels.UserPanel;
using EP.Domain.Interfaces.User;
using Microsoft.AspNetCore.Http;
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

        public SideBarViewModel GetSideBarInfromationByUserId(string userId);

        public ChangeAvatarServiceModel UploadImageAndDeletePreviousOne(IFormFile newAvatar, string previousAvatar);
    }
}
