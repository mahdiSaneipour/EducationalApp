using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP.Domain.Entities.Course
{
    public class CourseComment
    {
        [Key]
        public int CM_Id { get; set; }

        [Display(Name = "متن نظر")]  
        [Required(ErrorMessage = "فیلد {0} نمیتواند خالی باشد")]
        [MaxLength(200,ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        public string CommentText { get; set; }

        [Display(Name = "خوانده شده توسط ادمین")]
        public bool IsAdminRead { get; set; }

        [Display(Name = "تاریخ")]
        public DateTime CreateDate { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int CourseId { get; set; }

        #region Relation

        public User.User User { get; set; }

        public Course Course { get; set; }

        #endregion

    }
}
