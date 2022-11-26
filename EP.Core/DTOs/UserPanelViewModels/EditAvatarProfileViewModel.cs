using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP.Core.DTOs.UserPanelViewModels
{
    public class EditAvatarProfileViewModel
    {
        [Display(Name = "انتخاب آواتار")]
        [Required(ErrorMessage = "فیلد {0} نمیتواند خالی باشد")]
        public IFormFile SelectedAvatarFile { get; set; }

        [Display(Name = "آواتار انتخاب شده")]
        public string PreviousSelectedAvatar { get; set; }
    }
}
