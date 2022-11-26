using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EP.Core.DTOs.UserPanelViewModels
{
    public class SideBarViewModel
    {
        [Display(Name = "نام کاربری")]
        public string Username { get; set; }

        [Display(Name = "تصویر")]
        public string ImageAddress { get; set; }

        [Display(Name = "تاریخ ثبت نام")]
        public DateTime RegisterDate { get; set; }
    }
}
