﻿using EP.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP.Domain.Entities.Wallet
{
    public class Wallet
    {
        public Wallet()
        {

        }

        [Key]
        public int WalletId { get; set; }

        [Display(Name = "نوع تراکنش")]
        [Required(ErrorMessage = "فیلد {0} نمیتواند خالی باشد")]
        public int TypeId { get; set; }
        
        [Display(Name = "کاربر")]   
        [Required(ErrorMessage = "فیلد {0} نمیتواند خالی باشد")]
        public int UserId { get; set; }

        [Display(Name = "مبلغ")]     
        [Required(ErrorMessage = "فیلد {0} نمیتواند خالی باشد")]
        public int Amount { get; set; }

        [Display(Name = "تایید شده")]     
        [Required(ErrorMessage = "فیلد {0} نمیتواند خالی باشد")]
        public bool IsPay { get; set; }

        [Display(Name = "شرح")]      
        [Required(ErrorMessage = "فیلد {0} نمیتواند خالی باشد")]
        [MaxLength(500,ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        public string Description { get; set; }

        [Display(Name = "تاریخ و ساعت")]
        public DateTime CreateDate { get; set; }

        #region Relations

        public virtual WalletType Type { get; set; }

        public virtual User.User User { get; set; }

        #endregion
    }
}
