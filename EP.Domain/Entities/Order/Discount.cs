using EP.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP.Domain.Entities.Order
{
    public class Discount
    {
        [Key]
        public int DiscountId { get; set; }

        [Required]
        public string DiscountCode { get; set; }

        [Required]
        public int DicountPercent { get; set; }

        public int? UsableCount { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        #region Relations

        public List<UserDiscounts> UserDiscounts { get; set; }

        #endregion

    }
}
