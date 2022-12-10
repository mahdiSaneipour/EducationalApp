using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP.Core.DTOs.UserPanelViewModels
{
    public class ChargeWalletViewModel
    {
        [Display(Name = "مبلغ")]  
        [Required(ErrorMessage = "فیلد {0} نمیتواند خالی باشد")]
        public int Amount { get; set; }
    }
}
