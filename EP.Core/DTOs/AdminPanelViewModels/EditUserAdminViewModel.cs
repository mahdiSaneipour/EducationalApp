﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EP.Core.DTOs.AdminPanelViewModels
{
    public class EditUserAdminViewModel
    {
        [Display(Name = "ایدی کاربر")]
        public int userId { get; set; }

        [Display(Name = "ایمیل")]
        public string Email { get; set; }

        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "فیلد {0} نمیتواند خالی باشد")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        public string Username { get; set; }

        [Display(Name = "رمز عبور")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        public string Password { get; set; }

        [Display(Name = "انتخاب تصویر")]
        [Required(ErrorMessage = "فیلد {0} نمیتواند خالی باشد")]
        public string SelectedAvatar { get; set; }

        [Display(Name = "نقش های کاربر")]
        public List<int> UserRoles { get; set; }
    }
}
