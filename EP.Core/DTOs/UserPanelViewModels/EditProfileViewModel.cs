using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EP.Core.DTOs.UserPanelViewModels
{
    public class EditProfileViewModel
    {
        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "فیلد {0} نمیتواند خالی باشد")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        public string Username { get; set; }

        [Display(Name = "انتخاب تصویر")]
        [Required(ErrorMessage = "فیلد {0} نمیتواند خالی باشد")]
        public string SelectedAvatar { get; set; }

        [Display(Name = "تصویر کاربر")]
        public string UserAvatar { get; set; }

    }
}
