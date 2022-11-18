using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP.Core.DTOs.AccountViewModels
{
    public class ActiveEmailViewModel
    {
        [Display(Name = "نام کاربری")]
        public string Username { get; set; }

        [Display(Name = "کد کاربری")]
        public string UserCode { get; set; }
    }
}
