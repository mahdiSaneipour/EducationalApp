using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP.Core.DTOs.AdminPanelViewModels
{
    public class CreateRoleViewModel
    {
        [Display(Name = "اسم نقش")]     
        [Required(ErrorMessage = "فیلد {0} نمیتواند خالی باشد")]
        [MaxLength(50,ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        public string roleName { get; set; }
    }
}
