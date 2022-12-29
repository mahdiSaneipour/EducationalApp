using EP.Domain.Entities.Course;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP.Domain.Entities.Order
{
    public class OrderDetails
    {
        [Key]
        public int DetailsId { get; set; }

        [Required]
        public int Price { get; set; }

        [Required]
        public int Count { get; set; }

        [Required]
        public int OrderId { get; set; }

        [Required]
        public int CourseId { get; set; }

        #region Relations

        [ForeignKey("OrderId")]
        public Order Order { get; set; }

        [ForeignKey("CourseId")]
        public Course.Course Course { get; set; }

        #endregion

    }
}
