using EP.Core.DTOs.UserPanelViewModels;
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

                    string avatarName = Tools.Generator.NameGenerator.GenerateUniqCode() + 
                        newAvatar.ContentType.Replace("image/", ".");

                    string avatarPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/profile-images", avatarName);

                    using (var stream = new FileStream(avatarPath, FileMode.Create))
                    {
                        newAvatar.CopyTo(stream);
                    }

                    return SetChangeAvatarServiceModel(ChangeAvatarEnums.Successful, avatarName); ;

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

        public EditProfileViewModel GetEditProfileInformation(string userId)
        {

            int id = Int32.Parse(userId);

            Domain.Entities.User.User user = new Domain.Entities.User.User();

            user = _userRepository.GetUserByUserId(id);

            EditProfileViewModel result = new EditProfileViewModel()
            {
                SelectedAvatar = user.UserAvatar,
                UserAvatar = user.UserAvatar,
                Username = user.UserName,
                UserId = user.UserId
            };

            return result;
        }

        public ChangeProfileEnums EditProfileByUserPanel(EditProfileViewModel profile)
        {
            int userId = profile.UserId;
            string username = profile.Username;
            string avatar = profile.SelectedAvatar;
            string previousAvatar = profile.UserAvatar;

            try {

                Domain.Entities.User.User user = new Domain.Entities.User.User();
                user = _userRepository.GetUserByUserId(userId);

                if (_userRepository.IsUserNameExist(username) && username != user.UserName)
                {
                    return ChangeProfileEnums.UsernameIsRepetitious;
                }

                if (previousAvatar != "Default.jpg")
                {
                    string previousAvatarPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/profile-images", previousAvatar);

                    if (File.Exists(previousAvatarPath))
                    {
                        File.Delete(previousAvatarPath);
                    }
                    else
                    {
                        return ChangeProfileEnums.PreviousImageNotFound;
                    }
                }



                user.UserName = username;
                user.UserAvatar = avatar;

                _userRepository.UpdateUser(user);
                _userRepository.SaveChanges();

                Console.WriteLine(user.UserName);

                return ChangeProfileEnums.Successful;

            }
            catch
            {
                return ChangeProfileEnums.ServerError;
            }
        }

        public ChangePasswordEnums ChangePassword(ChangePasswordViewModel model)
        {
            try
            {
                string currentPassword = Tools.Security.PasswordHelper.EncodePasswordMd5(model.CurrentPassword);
                string password = Tools.Security.PasswordHelper.EncodePasswordMd5(model.Password);
                int userId = model.userId;

                if (_userRepository.CheckPassword(currentPassword, userId))
                {
                    return ChangePasswordEnums.CurrentPasswordIsNotTrue;
                }

                Domain.Entities.User.User user = _userRepository.GetUserByUserId(userId);

                user.Password = password;

                _userRepository.UpdateUser(user);
                _userRepository.SaveChanges();

                return ChangePasswordEnums.Successful;
            }
            catch
            {
                return ChangePasswordEnums.ServerError;
            }
        }
    }
}
