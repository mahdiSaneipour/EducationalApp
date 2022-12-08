using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP.Core.DTOs.UserPanelViewModels
{
    public class ChangePasswordViewModel
    {
        [Display(Name = "ایدی کاربر")]
        public int userId { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "رمزعبور فعلی")]
        public string CurrentPassword { get; set; }

        [Required]
        [Display(Name = "رمزعبور")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "تایید رمز عبور")]
        [Compare("Password", ErrorMessage = "تکرار رمزعبور با رمز عبور مطابقت ندارند")]
        public string ConfirmPassword { get; set; }
    }
}
