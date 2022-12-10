using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP.Core.DTOs.UserPanelViewModels
{
    public class WalletViewModel
    {
        [Display(Name = "مبلغ")]
        public int Amount { get; set; }

        [Display(Name = "نوع")]
        public int Type { get; set; }

        [Display(Name = "شرح")]
        public string Description { get; set; }

        [Display(Name = "تاریخ")]
        public DateTime CreateDate { get; set; }
    }
}
