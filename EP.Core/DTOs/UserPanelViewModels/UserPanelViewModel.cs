using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP.Core.DTOs.UserPanelViewModels
{
    public class UserPanelViewModel
    {
        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "فیلد {0} نمیتواند خالی باشد")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        public string Username { get; set; }

        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "فیلد {0} نمیتواند خالی باشد")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        public string Email { get; set; }

        [Display(Name = "تاریخ ثبت نام")]
        [Required(ErrorMessage = "فیلد {0} نمیتواند الی باشد")]
        [MaxLength(200,ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        public DateTime RegisterDate { get; set; }

        [Display(Name = "موجودی کیف پول")]
        [Required(ErrorMessage = "{0} نمیتواند خالی باشد")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        public int Wallet { get; set; }
    }
}
