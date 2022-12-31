using EP.Domain.Entities.Order;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP.Domain.Entities.User
{
    public class UserDiscounts
    {
        [Key]
        public int UD_Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int DiscountId { get; set; }

        #region

        public User User { get; set; }

        public Discount Discount { get; set; }

        #endregion

    }
}
