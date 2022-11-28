﻿using EP.Core.DTOs.UserPanelViewModels;
using EP.Core.Enums.UserPanel;
using EP.Core.Interfaces.User;
using EP.Core.ServiceModels.UserPanel;
using EP.Domain.Interfaces.User;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP.Core.Services.User
{
    public class UserPanelServices : IUserPanelServices
    {
        private readonly IUserRepository _userRepository;

        public UserPanelServices(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public SideBarViewModel GetSideBarInfromationByUserId(string userId)
        {
            SideBarViewModel result = new SideBarViewModel();

            int id = Convert.ToInt32(userId);
            Domain.Entities.User.User user = _userRepository.GetUserByUserId(id);

            result.RegisterDate = user.RegisterDate;
            result.ImageAddress = user.UserAvatar;
            result.Username = user.UserName;

            return result;
        }

        public UserPanelViewModel GetUserInformationForUserPanel(string userId)
        {
            UserPanelViewModel result = new UserPanelViewModel();

            int id = Convert.ToInt32(userId);
            Domain.Entities.User.User user = _userRepository.GetUserByUserId(id);

            result.RegisterDate = user.RegisterDate;
            result.Username = user.UserName;
            result.Email = user.Email;
            result.Wallet = 0;

            return result;
        }

        public ChangeAvatarServiceModel UploadImageAndDeletePreviousOne(IFormFile newAvatar, string previousAvatar)
        {

            try
            {
                if (newAvatar != null)
                {
                    if (previousAvatar != "NoPreviousAvatar")
                    {
                        string previousAvatarPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/profile-images", previousAvatar);

                        if (File.Exists(previousAvatarPath))
                        {
                            File.Delete(previousAvatarPath);
                        }
                        else
                        {
                            return SetChangeAvatarServiceModel(ChangeAvatarEnums.PreviousAvatarNotFound, ""); ;
                        }

                    }

                    string avatarName = Path.Combine(Tools.Generator.NameGenerator.GenerateUniqCode()
                        ,newAvatar.ContentType.Replace("image/", "."));
                    string avatarPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/profile-images", avatarName);

                    return SetChangeAvatarServiceModel(ChangeAvatarEnums.Successful, avatarPath); ;

                }
                else
                {
                    return SetChangeAvatarServiceModel(ChangeAvatarEnums.AvatarFileIsNull, ""); ;
                }
            }
            catch
            {
                return SetChangeAvatarServiceModel(ChangeAvatarEnums.ServerError, ""); ;
            }

            
        }

        public ChangeAvatarServiceModel SetChangeAvatarServiceModel(ChangeAvatarEnums status, string avatarAddress)
        {
            ChangeAvatarServiceModel result = new ChangeAvatarServiceModel();

            result.AvatarAddress = avatarAddress;
            result.Status = status;

            return result;
        }
    }
}
